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
        [TestCase("string \"hello world\" | replace o 0", "hell0 w0rld")]
        [TestCase("string 10 | num | string | num | string | num | add 10", "20")]
        [TestCase("num 0 | bool | string", "False")]
        [TestCase("num 100 | mult 2 | add 1", "201")]
        [TestCase("num 100 | great 99", "True")]
        [TestCase("&num value 10 | great 9", "True")]
        [TestCase("&string text \"Hello World\" | replace -i h J", "Jello World")]
        
        public void SingleLineTest(string commandInput, string output)
        {
            var result = Program.RunSingleLine(new Context
            {
                Command = null,
                RuntimeMode = RuntimeMode.COMMANDLINE
            }, commandInput);
            
            if (result.Error != null)
            {
                Assert.IsNull(result.Error, $"Input: {commandInput}");
            }
            Assert.AreEqual(output, result.Value);
        }
    }
}