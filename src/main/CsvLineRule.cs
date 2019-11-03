using entities;
using mxcd.core.rules;
using System;
using System.Threading.Tasks;

namespace model
{
    public class CsvLineRule : IRule<ICsvLine>
    {
        readonly Func<ICsvLine, bool> _toCheck;
        public CsvLineRule(Func<ICsvLine, bool> toCheck)
        {
            _toCheck = toCheck;
        }
        public async Task<bool> Check(ICsvLine obj)
        {
            return _toCheck(obj);
        }

    }
}
