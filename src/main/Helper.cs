using entities;
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
            DataRules = dataRules ?? new List<IRule<ICsvData>>();
            LineRules = lineRules ?? new List<IRule<ICsvLine>>();
            Actions = actions ?? new List<Action<ICsvLine>>();
            Config = config;
        }

        public IConfiguration ConvertConfig(string[] data)
        {
            var content = string.Join(" ", data);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(content);
        }

        public IConfiguration ConvertConfig(string data)
        {
            return ConvertConfig(new string[] { data });
        }

        public ICsvData ConvertData(string[] data)
        {
            if (this.Config == null) throw new ArgumentException("Config can not be null");

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
            if (this.Config == null) throw new ArgumentException("Config can not be null");
            
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
                var resultLines = new List<ICsvLine>();

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
                    resultLines.Add(line);
                }

                lines = DataToString(data.Keys, resultLines, errors, Config.output);
            }


            return new Result
            {
                Data = lines,
                Errors = errors?.OrderBy(x=>x.Index).Select(x=>x.ToString()).ToArray()
            };
        }

        private static string[] DataToString(IEnumerable<string> keys,IEnumerable<ICsvLine> lines, IEnumerable<ErrorLine> errors, IOutputConfig config)
        {
            errors = errors ?? new List<ErrorLine>();
            var delimiter = config.delimiter.ToString();
            var oResult = new List<string>() { string.Join(delimiter, keys) };

            var errorLines = errors
                .Where(x => x.Index != null)
                .Select(x => x.Index)
                .Distinct()
                .ToList();
            var i = 0;
            foreach (var line in lines)
            {
                var linetoAdd = new string[keys.Count()];
                if (!errorLines.Contains(i++))
                {
                    var j = 0;
                    foreach (var key in keys)
                    {
                        linetoAdd[j++] = line[key];
                    }

                    oResult.Add(string.Join(delimiter, linetoAdd));
                }
            }

            return oResult.ToArray();
        }
    }
}
