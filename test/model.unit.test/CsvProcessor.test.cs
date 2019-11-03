
using System.IO;
using System.Linq;
using Xunit;

namespace model.test
{
    
    public class CsvProcessorTests
    {
        [Fact]
        public void Process()
        {
            // Arrange
            var csvReader = new CsvProcesor();
            string urlFile = $"{Directory.GetCurrentDirectory()}/files/myCSV.csv";
            char delimiter = ';';

            var data = System.IO.File.ReadAllLines(urlFile);

            // Act
            var result = csvReader.Process(
                data,
                delimiter);

            // Assert
            Assert.True(result.Keys.Any());
            Assert.True(result.Values.Any());

        }
    }
}
