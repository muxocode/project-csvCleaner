using entities;
using System.Collections.Generic;

namespace model
{
    public class OutputConfig : ConfigSection, IOutputConfig
    {
        public IEnumerable<TupleReplace> replaces { get; set; }
        public IEnumerable<Condition> conditions { get; set; }
        public IEnumerable<TypeColumn> types { get; set; }

        IEnumerable<ITupleReplace> IOutputConfig.replaces => replaces;
        IEnumerable<ICondition> IOutputConfig.conditions => conditions;
        IEnumerable<ITypeColumn> IOutputConfig.types => types;
    }
}
