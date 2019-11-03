using System;

namespace model.CsvException
{
    public class FileFormatException : Exception
    {
        public FileFormatException(string text) : base(text)
        {

        }
    }
}
