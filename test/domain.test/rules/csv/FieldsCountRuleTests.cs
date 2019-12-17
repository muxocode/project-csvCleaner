using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using model.rules.csv;
using System.Collections.Generic;
using System.Threading.Tasks;
using model.CsvException;

namespace model.test.rules.csv
{
    [TestClass]
    public class FieldsCountRuleTests
    {
        private FieldsCountRule CreateFieldsCountRule()
        {
            return new FieldsCountRule();
        }

        [TestMethod]
        public async Task CountTest()
        {
            // Arrange
            var fieldsCountRule = this.CreateFieldsCountRule();

            var csvDataOk = new CsvData();
            var aListA = new List<string>() { "A1", "A2", "A3", "A4" };
            var aListB = new List<string>() { "B1", "B2", "B3", "B4" };
            csvDataOk.Add("A", aListA);
            csvDataOk.Add("B", aListB);

            var csvDataWrong = new CsvData();
            var aListC = new List<string>() { "C1" };
            csvDataWrong.Add("A", aListA);
            csvDataWrong.Add("B", aListB);
            csvDataWrong.Add("C", aListC);



            // Assert
            Assert.IsTrue(await fieldsCountRule.Check(csvDataOk));
            await Assert.ThrowsExceptionAsync<FileFormatException>(()=>fieldsCountRule.Check(csvDataWrong));

        }
    }
}
