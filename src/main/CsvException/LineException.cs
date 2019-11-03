using System;

namespace model.CsvException
{
    public class LineException : Exception
    {
        public LineException(string text) : base(text)
        {

        }
    }
}
