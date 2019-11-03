using Xunit;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace model.test
{
    
    public class HelperTests
    {
        [Fact]
        public void ReadConfig()
        {
            // Arrange
            var helper = new Helper();
            string urlFile = $"{Directory.GetCurrentDirectory()}/files/complete.json";

            // Act
            var result = helper.ReadConfig(urlFile);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ReadCsv()
        {
            // Arrange
            var helper = new Helper();
            string urlFile = $"{Directory.GetCurrentDirectory()}/files/myCSV.csv";

            // Act
            var result = helper.ReadCsv(urlFile);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
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
            Assert.Contains(",",result[0]);
            Assert.True(result[0].Split(",").Count() == 3);
            Assert.True(result.Count() == 9);

        }
    }
}
