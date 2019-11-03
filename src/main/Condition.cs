using entities;
using System.Collections.Generic;

namespace model
{

    public class Condition : ICondition
    {
        public string column { get; set; }
        public string condition { get; set; }
        public string value { get; set; }

    }
}
