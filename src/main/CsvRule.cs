using entities;
using mxcd.core.rules;
using System;
using System.Threading.Tasks;

namespace model
{
    public class CsvRule : IRule<ICsvData>
    {
        readonly Func<ICsvData, bool> _toCheck;
        public CsvRule(Func<ICsvData, bool> toCheck)
        {
            _toCheck = toCheck;
        }
        public async Task<bool> Check(ICsvData obj)
        {
            return await Task.Run(() => _toCheck(obj));
        }
    }
}
