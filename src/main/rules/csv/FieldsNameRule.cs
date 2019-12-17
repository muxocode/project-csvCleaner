using System.Linq;

namespace model.rules.csv
{
    public class FieldsNameRule : CsvRule
    {
        private const string Message = "Existen dos columnas con el mismo nombre";

        public FieldsNameRule() : base(x =>
        {
            if (x.Keys.Distinct().Count() != x.Keys.Count)
            {
                throw new CsvException.FileFormatException(Message);
            }

            return true;
        })
        { }
    }
}
