namespace entities
{
    public interface ICondition
    {
        string column { get; set; }
        string condition { get; set; }
        string value { get; set; }
    }
}