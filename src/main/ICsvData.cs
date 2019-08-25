using System;
using System.Collections.Generic;
using System.Text;

namespace domain
{
    public interface ICsvData: IDictionary<string, IEnumerable<string>>
    {
    }

    internal class CsvData : Dictionary<string, IEnumerable<string>>, ICsvData
    {

    }
}
