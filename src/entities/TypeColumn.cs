using System;
using System.Collections.Generic;
using System.Text;

namespace entities
{
    public enum ETypeColumn
    {
        number,
        text
    }

    public class TypeColumn
    {
        public string column { get; set; }
        public ETypeColumn type { get; set; }
    }
}
