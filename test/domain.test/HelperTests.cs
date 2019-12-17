using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using entities;
using model;
using mxcd.core.rules;
using System.IO;
using System.Linq;

namespace model.test
{
    [TestClass]
    public class HelperTests
    {
        private MockRepository mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.mockRepository.VerifyAll();
        }

        IConfiguration GetConfig()
        {
            // Arrange
            var helper = new Helper(null, null, null, null);

            // Act
            var content = System.IO.File.ReadAllText($"{Directory.GetCurrentDirectory()}/files/complete.json");
            return helper.ConvertConfig(content);
        }

        [TestMethod]
        public void ConvertConfig()
        {
            // Assert
            Assert.IsNotNull(GetConfig());

        }

        [TestMethod]
        public void ConvertData()
        {
            // Arrange
            var mockConfiguration = this.mockRepository.Create<IConfiguration>();
            var mockInputConfig = this.mockRepository.Create<IInputConfig>();

            //Input config
            mockInputConfig.Setup(x => x.delimiter).Returns(';');

            //configuration
            mockConfiguration
                .Setup(x => x.input)
                .Returns(() => mockInputConfig.Object);

            var helper = new Helper(null, null, null, mockConfiguration.Object);
            string[] data = System.IO.File.ReadAllLines($"{Directory.GetCurrentDirectory()}/files/myCSV.csv");
           

            // Act
            var result = helper.ConvertData(data);

            // Assert
            Assert.IsTrue(result.Keys.Count == 14);
        }

        [TestMethod]
        public async Task Generate()
        {
            // Arrange
            var mockConfiguration = this.mockRepository.Create<IConfiguration>();
            var mockInputConfig = this.mockRepository.Create<IInputConfig>();
            var mockOutputConfig = this.mockRepository.Create<IOutputConfig>();



            //config
            mockInputConfig.Setup(x => x.delimiter).Returns(';');
            mockOutputConfig.Setup(x => x.delimiter).Returns('#');

            //configuration
            mockConfiguration
                .Setup(x => x.input)
                .Returns(() => mockInputConfig.Object);

            mockConfiguration
                .Setup(x => x.output)
                .Returns(() => mockOutputConfig.Object);



            var helper = new Helper(null, null, null, mockConfiguration.Object);
            ICsvData data = helper.ConvertData(System.IO.File.ReadAllLines($"{Directory.GetCurrentDirectory()}/files/myCSV.csv"));

            // Act
            var result = await helper.Generate(data);


            // Assert

            //Campos

            var i = 0;

            foreach (var item in data.GetLines().First().Keys)
            {
                var val = result.Data[0].Split(mockOutputConfig.Object.delimiter)[i++];
                Assert.IsTrue(item == val);
            }

            i = 0;

            foreach (var line in data.GetLines())
            {
                i++;
                var j = 0;
                foreach (var item in line)
                {
                    var val = result.Data[i].Split(mockOutputConfig.Object.delimiter)[j++];
                    Assert.IsTrue(item.Value == val);
                }
            }
        }
    }
}
