using entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using mxcd.core.rules;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace model.test
{
    static class Extensions
    {
        public static IEnumerable<Condition> AddEqual(this IEnumerable<Condition> conditions, string column, string value)
        {
            conditions = conditions.Append(new Condition
            {
                column = column,
                condition = ConditionalOperators.equal,
                value = value
            });
            return conditions;
        }
        public static IEnumerable<Condition> AddNotEqual(this IEnumerable<Condition> conditions, string column, string value)
        {
            conditions = conditions.Append(new Condition
            {
                column = column,
                condition = ConditionalOperators.notEqual,
                value = value
            });
            return conditions;
        }
        public static IEnumerable<Condition> AddGreater(this IEnumerable<Condition> conditions, string column, string value)
        {
            conditions = conditions.Append(new Condition
            {
                column = column,
                condition = ConditionalOperators.greater,
                value = value
            });
            return conditions;
        }

        public static IEnumerable<Condition> AddNotGreaterEqual(this IEnumerable<Condition> conditions, string column, string value)
        {
            conditions = conditions.Append(new Condition
            {
                column = column,
                condition = ConditionalOperators.greaterEqual,
                value = value
            });
            return conditions;
        }

        public static IEnumerable<Condition> AddMinor(this IEnumerable<Condition> conditions, string column, string value)
        {
            conditions = conditions.Append(new Condition
            {
                column = column,
                condition = ConditionalOperators.minor,
                value = value
            });
            return conditions;
        }

        public static IEnumerable<Condition> AddMinorEqual(this IEnumerable<Condition> conditions, string column, string value)
        {
            conditions = conditions.Append(new Condition
            {
                column = column,
                condition = ConditionalOperators.minorEqual,
                value = value
            });
            return conditions;
        }

        public static IEnumerable<ICsvLine> AddStringLine(this IEnumerable<ICsvLine> lines, string repeatValue = null)
        {
            var csvData = new CsvData();
            var aListA = repeatValue == null ? new List<string>() { "S1", "S2", "S3", "S4" } : new List<string>() { repeatValue, repeatValue, repeatValue, repeatValue };
            csvData.Add("string", aListA);


            lines = lines.Append(csvData.GetLines().First());
            return lines;
        }

        public static IEnumerable<ICsvLine> AddIntLine(this IEnumerable<ICsvLine> lines, int? repeatValue = null)
        {
            var csvData = new CsvData();
            var aListA = repeatValue == null ? new List<string>() { "1", "2", "3", "4" } : new List<string>() { repeatValue.ToString(), repeatValue.ToString(), repeatValue.ToString(), repeatValue.ToString() };

            csvData.Add("int", aListA);


            lines = lines.Append(csvData.GetLines().First());
            return lines;
        }

        public static IEnumerable<ICsvLine> AddFloatLine(this IEnumerable<ICsvLine> lines, float? repeatValue = null)
        {
            var csvData = new CsvData();
            var aListA = repeatValue == null ? new List<string>() { "1.1", "2.2", "3.3", "4.4" } : new List<string>() { repeatValue.ToString(), repeatValue.ToString(), repeatValue.ToString(), repeatValue.ToString() };

            csvData.Add("float", aListA);


            lines = lines.Append(csvData.GetLines().First());
            return lines;
        }

        public static async Task CheckRules(this IEnumerable<ICsvLine> lines, IEnumerable<IRule<ICsvLine>> rules)
        {
            foreach (var line in lines)
            {
                foreach (var rule in rules)
                {
                    Assert.IsTrue(await rule.Check(line));
                }
            }
        }
    }

    [TestClass]
    public class CsvLineRuleCreatorTests
    {

        private IEnumerable<ICsvLine> CreateEmptyLines()
        {
            var csvData = new CsvData();
            return csvData.GetLines();
        }



        [TestMethod]
        public void Create_null()
        {
            // Arrange
            IEnumerable<Condition> conditions = null;

            // Act
            var rules = CsvLineRuleCreator.Create(conditions);

            // Assert
            Assert.IsFalse(rules.Any());
        }

        [TestMethod]
        public async Task check_string_empty()
        {
            // Arrange
            var conditions = new List<Condition>();
            var lines = CreateEmptyLines();

            // Act
            var rules = CsvLineRuleCreator.Create(conditions);

            // Assert
            await lines.CheckRules(rules);
        }

        [TestMethod]
        public async Task check_string_equal()
        {
            // Arrange
            var conditions = new List<Condition>();
            conditions.AddEqual("S", "x");


            var lines = CreateEmptyLines();
            lines.AddStringLine("x");

            // Act
            var rules = CsvLineRuleCreator.Create(conditions);

            // Assert
            await lines.CheckRules(rules);
        }

        [TestMethod]
        public async Task check_string_equal_error()
        {
            // Arrange
            var conditions = new List<Condition>();
            conditions.AddEqual("S", "x");


            var lines = CreateEmptyLines();
            lines.AddStringLine();

            // Act
            var rules = CsvLineRuleCreator.Create(conditions);

            // Assert
            await lines.CheckRules(rules);
        }
    }
}
