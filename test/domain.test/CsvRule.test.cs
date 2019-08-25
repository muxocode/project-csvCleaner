using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using domain;

namespace domain.test
{
    [TestClass]
    public class CsvRuleTests
    {
        [TestMethod]
        public async Task True()
        {
            // Arrange
            var csvRule = new CsvRule(x=>true);
            ICsvData obj = null;

            // Act
            var result = await csvRule.Check(obj);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public async Task False()
        {
            // Arrange
            var csvRule = new CsvRule(x=>false);
            ICsvData obj = null;

            // Act
            var result = await csvRule.Check(obj);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
