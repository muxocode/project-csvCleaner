using entities;

namespace model
{
    public class TupleReplace : ITupleReplace
    {
        public string column { get; set; }
        public string from { get; set; }
        public string to { get; set; }

    }
}
