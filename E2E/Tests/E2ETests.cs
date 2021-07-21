using NUnit.Framework;
using kasic;
using kasic.Logging;

namespace E2E.Tests
{
    public class E2ETests
    {
        private static TestData[] data = new[]
        {
            new TestData
            {
                CommandInput = "add 10 10",
                ExpectedResult = "20"
            },
            new TestData
            {
                CommandInput = "num 10 | add -123 | string",
                ExpectedResult = "-113"
            },
            new TestData
            {
                CommandInput = "string 20d | replace d 0 | num | add 200",
                ExpectedResult = "400"
            },
        };

        [Test]
        public void SingleLineTest()
        {
            foreach (var iter in data)
            {
                var result = Program.RunLine(iter.CommandInput);
                if (result.Error != null)
                {
                    Assert.IsNull(result.Error, $"Input: {iter.CommandInput}");
                }
                Assert.AreEqual(iter.ExpectedResult, result.Value);
            }
        }
    }

    public struct TestData
    {
        public string CommandInput;
        public string ExpectedResult;
    }
}