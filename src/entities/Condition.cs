using System;
using System.Collections.Generic;
using System.Text;

namespace entities
{
    public static class ConditionalOperators
    {
        public const string equal = "==";
        public const string notEual = "!=";
        public const string greaterEqual = ">=";
        public const string minorEqual = "<=";
        public const string greater = ">";
        public const string minor = "<";
    }
    public class Condition
    {
        public string column { get; set; }
        public string condition { get; set; }
        public string value { get; set; }

    }
}
