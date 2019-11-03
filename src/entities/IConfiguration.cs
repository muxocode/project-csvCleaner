namespace entities
{
    public interface IConfiguration
    {
        IInputConfig input { get; }
        IOutputConfig output { get; }
    }
}
