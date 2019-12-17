using entities;

namespace model
{
    public class ErrorLine : IErrorCsv
    {
        readonly string Error;
        public long? Index { get; }


        public ErrorLine(string error, long? index = null)
        {
            this.Error = error;
            this.Index = index;
        }

        public override string ToString()
        {
            var text = this.Index.HasValue ? $" in line {this.Index}" : "";
            return $@"
****************************************************************
ERROR {text}

{this.Error}

****************************************************************

";
        }
    }
}
