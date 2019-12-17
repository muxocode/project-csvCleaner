using System.Threading.Tasks;

namespace entities
{
    public interface IAppHelper
    {
        ICsvData ConvertData(string[] data);
        Task<IResult> Generate(ICsvData data);
    }

    public interface IConfigHelper
    {
        IConfiguration ConvertConfig(string[] data);
        IConfiguration ConvertConfig(string data);

    }
}