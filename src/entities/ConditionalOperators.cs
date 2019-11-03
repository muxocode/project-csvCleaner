using System;
using System.Collections.Generic;
using System.Text;

namespace entities
{
    public static class ConditionalOperators
    {
        public const string equal = "==";
        public const string notEqual = "!=";
        public const string greaterEqual = ">=";
        public const string minorEqual = "<=";
        public const string greater = ">";
        public const string minor = "<";

        public static IEnumerable<string> GetEnum()
        {
            return new List<string>()
            {
                equal,
                notEqual,
                greaterEqual,
                minorEqual,
                greater,
                minor
            };
        }
    }
}
