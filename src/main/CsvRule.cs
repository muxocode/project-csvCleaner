using mxcd.core.rules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace domain
{
    public class CsvRule : IRule<ICsvData>
    {
        readonly Func<ICsvData, bool> _toCheck;
        public CsvRule(Func<ICsvData,bool> toCheck)
        {
            _toCheck = toCheck;
        }
        public async Task<bool> Check(ICsvData obj)
        {
            return _toCheck(obj);
        }
    }
}
