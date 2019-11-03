using System.Collections.Generic;

namespace entities
{
    public interface ICsvData : IDictionary<string, IEnumerable<string>>
    {
        IEnumerable<ICsvLine> GetLines();
    }
}
