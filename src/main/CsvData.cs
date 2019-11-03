using entities;
using System.Collections.Generic;
using System.Linq;

namespace model
{
    public class CsvData : Dictionary<string, IEnumerable<string>>, ICsvData
    {
        class CsvLines : Dictionary<string, string>, ICsvLine
        {

        }
        public IEnumerable<ICsvLine> GetLines()
        {
            var aLines = new List<CsvLines>();
            if (this.Values.Any())
            {
                for (int i = 0; i < this.Values.First().Count(); i++)
                {
                    var line = new CsvLines();
                    foreach (var item in this.Keys)
                    {
                        line.Add(item, (this[item] as List<string>)[i]);
                    }

                    aLines.Add(line);
                }
            }

            return aLines;
        }
    }
}
