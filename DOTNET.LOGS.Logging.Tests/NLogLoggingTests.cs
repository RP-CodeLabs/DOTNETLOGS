using System.Linq;
using NLog;
using NLog.Targets;
using NUnit.Framework;

namespace DOTNET.LOGS.Logging.Tests
{
    [TestFixture]
    public class NLogLoggingTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            NLogger.GetFor("memory").Setup(LogTarget.Memory);
        }

        [TestCase("Info")]
        [TestCase("Warn")]
        [TestCase("Fatal")]
        [TestCase("Debug")]
        [TestCase("Error")]
        public static void write_message_to_log(string key)
        {
            NLogLogging.GetFor(key).Log("Test data");
            var logFileContent  = LogManager.Configuration.FindTargetByName<MemoryTarget>("memory");
            Assert.True(logFileContent.Logs.Any(a => a.Contains(key.ToUpper())));
        }
    }
}
