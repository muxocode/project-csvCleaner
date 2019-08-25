using System.Collections.Generic;
using System.Linq;

namespace domain
{
    public class CsvProcesor
    {
        public ICsvData Process(string[] data, char delimiter)
        {
            CsvData oDictionary = null;
            List<string> Keys = null;

            foreach(var line in data)
            {
                    string[] values = line.Split(delimiter);

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
    }
}
