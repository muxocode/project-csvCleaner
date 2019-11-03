using System;
using System.Collections.Generic;
using System.Text;

namespace entities
{
    public interface IOutputConfig : IConfigSection
    {
        IEnumerable<ITupleReplace> replaces { get; }
        IEnumerable<ICondition> conditions { get; }
        IEnumerable<ITypeColumn> types { get; }
    }
}
