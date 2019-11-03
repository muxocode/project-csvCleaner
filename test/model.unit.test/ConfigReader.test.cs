using entities;

using System.IO;
using System.Linq;
using Xunit;

namespace model.test
{
    
    public class ConfigReaderTest
    {
        public Configuration Read(string urlFile)
        {
            var content = System.IO.File.ReadAllText(urlFile);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(content);
        }

        [Fact]
        public void EmpyFile()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/empty.json";
            var json = Read(url);

            Assert.Null(json.input);
            Assert.Null(json.output);
        }

        [Fact]
        public void Simple()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/simple.json";
            var json = Read(url);

            Assert.NotNull(json.input.delimiter);
            Assert.NotNull(json.output.delimiter);
        }

        [Fact]
        public void Replaces()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/complete.json";
            var json = Read(url);

            Assert.True(json.input.replaces.Count() == 2);
        }

        [Fact]
        public void Conditions()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/complete.json";
            var json = Read(url);

            Assert.True(json.input.conditions.Count() == 2);
        }

        [Fact]
        public void types()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/complete.json";
            var json = Read(url);

            Assert.True(json.input.types.Count() == 2);
        }
    }
}
