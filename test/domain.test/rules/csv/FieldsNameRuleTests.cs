using Microsoft.VisualStudio.TestTools.UnitTesting;
using model.CsvException;
using model.rules.csv;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace model.test.rules.csv
{
    [TestClass]
    public class FieldsNameRuleTests
    {
        [TestMethod]
        public async Task NameTest()
        {
            // Arrange
            var fieldsNameRule = new FieldsNameRule();

            // Act
            var csvDataOk = new CsvData();
            var aListA = new List<string>() { "A1", "A2", "A3", "A4" };
            var aListB = new List<string>() { "B1", "B2", "B3", "B4" };
            csvDataOk.Add("A", aListA);
            csvDataOk.Add("B", aListB);


            // Assert
            Assert.IsTrue(await fieldsNameRule.Check(csvDataOk));

        }
    }
}
