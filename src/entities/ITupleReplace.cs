namespace entities
{
    public interface ITupleReplace
    {
        string column { get; set; }
        string from { get; set; }
        string to { get; set; }
    }
}