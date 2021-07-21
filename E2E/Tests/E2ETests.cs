using NUnit.Framework;
using kasic;
using kasic.Kasic;
using kasic.Logging;

namespace E2E.Tests
{
    public class E2ETests
    {
        [TestCase("add 10 10", "20")]
        [TestCase("num 10 | add -123 | string", "-113")]
        [TestCase("string 20d | replace d 0 | num | add 200", "400")]
        public void SingleLineTest(string commandInput, string output)
        {
            var result = Program.RunLine(Context.CommandLineContext, commandInput);
            if (result.Error != null)
            {
                Assert.IsNull(result.Error, $"Input: {commandInput}");
            }
            Assert.AreEqual(output, result.Value);
        }
    }
}