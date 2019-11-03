﻿using entities;
using mxcd.core.rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model
{
    public class Helper : IAppHelper, IConfigHelper
    {
        IEnumerable<IRule<ICsvData>> DataRules { get; }
        IEnumerable<IRule<ICsvLine>> LineRules { get; }
        IEnumerable<Action<ICsvLine>> Actions { get; }
        IConfiguration Config { get; }

        public Helper(IEnumerable<IRule<ICsvData>> dataRules, IEnumerable<IRule<ICsvLine>> lineRules, IEnumerable<Action<ICsvLine>> actions, IConfiguration config)
        {
            DataRules = dataRules;
            LineRules = lineRules;
            Actions = actions;
            Config = config;
        }

        public IConfiguration ConvertConfig(string[] data)
        {
            var content = string.Join(" ", data);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(content);
        }

        public ICsvData ConvertData(string[] data)
        {
            CsvData oDictionary = null;
            List<string> Keys = null;

            foreach (var line in data)
            {
                string[] values = line.Split(Config.input.delimiter);

                if (oDictionary == null)
                {
                    oDictionary = new CsvData();
                    foreach (var val in values)
                    {
                        oDictionary.Add(val, new List<string>());
                    }
                    Keys = oDictionary.Keys.ToList();
                }
                else
                {
                    var i = 0;
                    foreach (var val in values)
                    {
                        (oDictionary[Keys[i++]] as List<string>).Add(val);
                    }
                }

            }

            return oDictionary;
        }



        public async Task<IResult> Generate(ICsvData data)
        {
            string[] lines = null;
            List<ErrorLine> errors = null;

            foreach (var item in DataRules)
            {
                try
                {
                    await item.Check(data);
                }
                catch (Exception oEx)
                {
                    errors = errors ?? new List<ErrorLine>();
                    errors.Add(new ErrorLine(oEx.Message));
                }
            }

            if(errors==null)
            {
                long i = 0;
                foreach (var line in data.GetLines())
                {
                    foreach (var item in LineRules)
                    {
                        try
                        {
                            await item.Check(line);
                        }
                        catch (Exception oEx)
                        {
                            errors = errors ?? new List<ErrorLine>();
                            errors.Add(new ErrorLine(oEx.Message,i));
                        }
                    }

                    foreach (var item in Actions)
                    {
                        await Task.Run(() =>
                        {
                            try
                            {
                                item(line);
                            }
                            catch (Exception oEx)
                            {
                                errors = errors ?? new List<ErrorLine>();
                                errors.Add(new ErrorLine(oEx.Message, i));
                            }
                        });
                    }

                    i++;
                }

                lines = DataToString(data, errors, Config.output);
            }


            return new Result
            {
                Data = lines,
                Errors = errors?.OrderBy(x=>x.Index).Select(x=>x.ToString()).ToArray()
            };
        }

        private static string[] DataToString(ICsvData data, IEnumerable<ErrorLine> errors, IOutputConfig config)
        {
            var delimiter = config.delimiter.ToString();
            var oResult = new List<string>() { string.Join(delimiter, data.Keys) };
            var n = 0;
            var errorLines = errors
                .Where(x=>x.Index!=null)
                .Select(x => x.Index)
                .Distinct()
                .ToList();

            for (var j = 0; j < data.Values.First().Count(); j++)
            {
                var line = new string[data.Keys.Count - errors.Count()];
                for (var i = 0; i < data.Keys.Count; i++)
                {
                    if (!errorLines.Contains(i))
                    {
                        var key = data.Keys.ToList()[i];
                        line[n++] = data[key].ToList()[j];
                    }

                }

                oResult.Add(string.Join(delimiter, line));
            }

            return oResult.ToArray();
        }
    }
}
