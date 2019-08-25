using System;
using System.Collections.Generic;

namespace entities
{
    public class InputConfig : ConfigSection
    {
        public IEnumerable<TupleReplace> replaces { get; set; }
        public IEnumerable<Condition> conditions { get; set; }
        public IEnumerable<TypeColumn> types { get; set; }
    }
}
