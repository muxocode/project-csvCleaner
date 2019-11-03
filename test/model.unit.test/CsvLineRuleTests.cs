using entities;

using Moq;
using System.Threading.Tasks;
using Xunit;

namespace model.test
{
    
    public class CsvLineRuleTests
    {
        [Fact]
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
            Assert.True(result);
        }
    }
}
