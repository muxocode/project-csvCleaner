using entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace domain.test
{
    [TestClass]
    public class ConfigReaderTest
    {
        public Configuration Read(string urlFile)
        {
            var content = System.IO.File.ReadAllText(urlFile);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Configuration>(content);
        }

        [TestMethod]
        public void EmpyFile()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/empty.json";
            var json = Read(url);

            Assert.IsNull(json.input);
            Assert.IsNull(json.output);
        }

        [TestMethod]
        public void Simple()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/simple.json";
            var json = Read(url);

            Assert.IsNotNull(json.input.delimiter);
            Assert.IsNotNull(json.output.delimiter);
        }

        [TestMethod]
        public void Replaces()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/complete.json";
            var json = Read(url);

            Assert.IsTrue(json.input.replaces.Count()==2);
        }

        [TestMethod]
        public void Conditions()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/complete.json";
            var json = Read(url);

            Assert.IsTrue(json.input.conditions.Count() == 2);
        }

        [TestMethod]
        public void types()
        {
            var url = $"{Directory.GetCurrentDirectory()}/files/complete.json";
            var json = Read(url);

            Assert.IsTrue(json.input.types.Count() == 2);
        }
    }
}
