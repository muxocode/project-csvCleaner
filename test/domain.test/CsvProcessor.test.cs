using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using domain;
using System.IO;
using System.Linq;

namespace domain.test
{
    [TestClass]
    public class CsvProcessorTests
    {
        [TestMethod]
        public void Process()
        {
            // Arrange
            var csvReader = new CsvProcesor();
            string urlFile = $"{Directory.GetCurrentDirectory()}/files/myCSV.csv";
            char delimiter = ';' ;

            var data = System.IO.File.ReadAllLines(urlFile);

            // Act
            var result = csvReader.Process(
                data,
                delimiter);

            // Assert
            Assert.IsTrue(result.Keys.Any());
            Assert.IsTrue(result.Values.Any());

        }
    }
}
