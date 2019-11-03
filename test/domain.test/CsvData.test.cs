using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace model.test
{
    [TestClass]
    public class CsvDataTests
    {
        [TestMethod]
        public void GetLines()
        {
            // Arrange
            var csvData = new CsvData();
            var aListA = new List<string>() { "A1", "A2", "A3", "A4" };
            var aListB = new List<string>() { "B1", "B2", "B3", "B4" };
            csvData.Add("A", aListA);
            csvData.Add("B", aListB);

            // Act
            var result = csvData.GetLines();


            // Assert
            for (int i = 0; i < aListA.Count; i++)
            {
                Assert.AreEqual(aListA[i], result.ToList()[i]["A"]);
                Assert.AreEqual(aListB[i], result.ToList()[i]["B"]);
            }
        }
    }
}
