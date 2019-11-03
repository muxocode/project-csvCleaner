using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace model.test
{
    /*
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void ReadConfig()
        {
            // Arrange
            var helper = new Helper();
            string urlFile = $"{Directory.GetCurrentDirectory()}/files/complete.json";

            // Act
            var result = helper.ReadConfig(urlFile);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReadCsv()
        {
            // Arrange
            var helper = new Helper();
            string urlFile = $"{Directory.GetCurrentDirectory()}/files/myCSV.csv";

            // Act
            var result = helper.ReadCsv(urlFile);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WriteCsv()
        {
            // Arrange
            var helper = new Helper();

            string delimiter = ",";

            var data = new CsvData();
            data.Add("col1", new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" });
            data.Add("col2", new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" });
            data.Add("col3", new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H" });

            // Act
            var result = helper.WriteCsv(
                data,
                delimiter);

            // Assert
            Assert.IsNotNull(result[0].Contains(","));
            Assert.IsNotNull(result[0].Split(",").Count() == 3);
            Assert.IsNotNull(result.Count() == 9);

        }
    }
    */
}
