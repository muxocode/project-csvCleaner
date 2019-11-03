using System;

namespace model.rules.line
{
    public class LineEmptyRule : model.CsvLineRule
    {
        public LineEmptyRule() : base(x =>
        {
            Func<string, bool> func = (string val) => val != null && val != String.Empty;

            var bResult = false;
            foreach (var item in x.Values)
            {
                bResult |= func(item);
            }

            if (!bResult)
            {
                throw new CsvException.LineException("Línea vacía");
            }

            return true;
        })
        {

        }
    }
}
