using entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace model.test
{
    [TestClass]
    public class CsvLineRuleTests
    {
        [TestMethod]
        public async Task Check()
        {
            // Arrange
            var text = "foo";
            var csvLineRule = new CsvLineRule(x =>
                x["A"] == text
            );
            var mock = new Mock<ICsvLine>();
            mock.SetupGet(p => p[It.IsAny<string>()]).Returns(text);
            ICsvLine obj = mock.Object;

            // Act
            var result = await csvLineRule.Check(obj);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
