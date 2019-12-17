using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using model;
using entities;
using System.Collections.Generic;
using System.Linq;

namespace model.test
{
    [TestClass]
    public class CsvActionCreatorTests
    {
        private MockRepository mockRepository;

        ITupleReplace CreateReplace(string from, string to, string column = null)
        {
            var obj = this.mockRepository.Create<ITupleReplace>();
            obj.SetupGet(x => x.from).Returns(from);
            obj.SetupGet(x => x.to).Returns(to);

            obj.SetupGet(x => x.column).Returns(column);

            return obj.Object;
        }

        ITypeColumn CreateType(ETypeColumn type, string column = null)
        {
            var obj = this.mockRepository.Create<ITypeColumn>();

            obj.SetupGet(x => x.type).Returns(type);
            obj.SetupGet(x => x.column).Returns(column);

            return obj.Object;
        }

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

        [TestMethod]
        public void Replace()
        {
            // Arrange
            IEnumerable<ITupleReplace> replaces = new List<ITupleReplace>()
            {
                CreateReplace("A","Z"),
                CreateReplace("B","V","B"),
            };

            var csvDataOk = new CsvData();
            var aListA = new List<string>() { "A1", "A2", "A3", "A4" };
            var aListB = new List<string>() { "AB1", "AB2", "AB3", "AB4" };
            csvDataOk.Add("A", aListA);
            csvDataOk.Add("B", aListB);
            var lines = csvDataOk.GetLines();

            // Act
            var result = CsvActionCreator.Create(
                replaces);



            // Assert
            foreach (var item in result)
            {
                lines.ToList().ForEach(item);
            }

            lines.ToList().ForEach(
                    x =>
                    {
                        Assert.IsTrue(x["A"].Contains("Z"));
                        Assert.IsTrue(!x["A"].Contains("A"));
                        Assert.IsTrue(x["B"].Contains("Z"));
                        Assert.IsTrue(!x["B"].Contains("A"));

                        Assert.IsTrue(x["B"].Contains("V"));
                        Assert.IsTrue(!x["B"].Contains("B"));
                    }
                );
        }

        [TestMethod]
        public void Types()
        {
            // Arrange
            IEnumerable<ITypeColumn> types = new List<ITypeColumn>()
            {
                CreateType(ETypeColumn.text,"Z"),
                CreateType(ETypeColumn.number,"B"),
                CreateType(ETypeColumn.number,"C")
            };

            var csvDataOk = new CsvData();
            var aListA = new List<string>() { "A1", "A2", "A3", "A4" };
            var aListB = new List<string>() { "1", "2", "3", "4" };
            var aListC = new List<string>() { "c1", "c2", "c3", "c4" };

            csvDataOk.Add("A", aListA);
            csvDataOk.Add("B", aListB);
            csvDataOk.Add("C", aListC);

            var lines = csvDataOk.GetLines();

            // Act
            var result = CsvActionCreator.Create(types).ToList();


            foreach (var item in lines)
            {
                result[0](item);
                result[1](item);
                Assert.ThrowsException<FormatException>(() => result[2](item));
            }

            // Assert
        }
    }
}
