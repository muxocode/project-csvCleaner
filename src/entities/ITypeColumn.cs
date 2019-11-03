namespace entities
{
    public enum ETypeColumn
    {
        number,
        text
    }

    public interface ITypeColumn
    {
        string column { get; set; }
        ETypeColumn type { get; set; }
    }
}
