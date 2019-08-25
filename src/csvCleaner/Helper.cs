using domain;
using entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csvCleaner
{
    public class Helper
    {
        public Configuration ReadConfig(string urlFile)
        {
            var content = System.IO.File.ReadAllText(urlFile);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(content);
        }
        public string[] ReadCsv(string urlFile)
        {
            return System.IO.File.ReadAllLines(urlFile);
        }
        public string[] WriteCsv(ICsvData data, string delimiter)
        {
            var oResult = new List<string>() { string.Join(delimiter, data.Keys) };

            for(var j = 0; j < data.Values.First().Count(); j++)
            {
                var line = new string[data.Keys.Count()];
                for (var i = 0; i < data.Keys.Count(); i++)
                {
                    var key = data.Keys.ToList()[i];
                    line[i] = data[key].ToList()[j];
                }

                oResult.Add(string.Join(delimiter, line));
            }

            return oResult.ToArray();
        }
    }
}
