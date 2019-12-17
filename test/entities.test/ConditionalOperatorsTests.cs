using System;
using Xunit;
using entities;
using System.Linq;

namespace entities.test
{
    public class ConditionalOperatorsTests
    {
        [Fact]
        public void GetEnum_StateUnderTest_ExpectedBehavior()
        {
            // Arrange

            // Act
            var result = ConditionalOperators.GetEnum();

            // Assert
            Assert.True(result.Any());

        }
    }
}
