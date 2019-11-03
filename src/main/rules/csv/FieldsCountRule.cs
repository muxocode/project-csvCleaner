using System.Linq;

namespace model.rules.csv
{
    public class FieldsCountRule : CsvRule
    {
        private const string Message = "La longitud de los resgistros no es uniforme, existen registros con difrente tamaño, por favor, revise si exiten textos que contengan el separador de csv";

        public FieldsCountRule() : base(x =>
        {
            var group = x.Values.GroupBy(y => y.Count());

            if (group.Select(y => y.Key).Distinct().Count() > 1)
            {
                throw new CsvException.FileFormatException(Message);
            }

            return true;
        })
        { }
    }
}
