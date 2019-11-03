using entities;

using System.Threading.Tasks;
using Xunit;

namespace model.test
{
    
    public class CsvRuleTests
    {
        [Fact]
        public async Task True()
        {
            // Arrange
            var csvRule = new CsvRule(x => true);
            ICsvData obj = null;

            // Act
            var result = await csvRule.Check(obj);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task False()
        {
            // Arrange
            var csvRule = new CsvRule(x => false);
            ICsvData obj = null;

            // Act
            var result = await csvRule.Check(obj);

            // Assert
            Assert.False(result);
        }
    }
}
