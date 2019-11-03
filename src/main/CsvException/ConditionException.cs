using System;


namespace model.CsvException
{
    public class ConditionException : Exception
    {
        public ConditionException(string text) : base(text)
        {

        }
    }
}

