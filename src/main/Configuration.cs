using entities;

namespace model
{
    public class Configuration:IConfiguration
    {
        public InputConfig input { get; set; }
        public OutputConfig output { get; set; }
        IInputConfig IConfiguration.input => input;

        IOutputConfig IConfiguration.output => output;
    }
}
