using entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    public class TypeColumn : ITypeColumn
    {
        public string column { get; set; }
        public ETypeColumn type { get; set; }
    }
}
