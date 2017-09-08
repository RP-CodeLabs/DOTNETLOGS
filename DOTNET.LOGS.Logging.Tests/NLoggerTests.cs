using System.Linq;
using NLog;
using NLog.Targets;
using NUnit.Framework;

namespace DOTNET.LOGS.Logging.Tests
{
    [TestFixture]
    public class NLoggerTests
    {
        [Test]
        public void setup_will_set_loggingconfiguration_with_memorytarget()
        {
            NLogger.GetFor("memory").Setup(LogTarget.Memory);
            Assert.True(LogManager.Configuration.AllTargets.Any(target => target is MemoryTarget));
        }

        [Test]
        public void setup_will_set_loggingconfiguration_with_filetarget()
        {
            NLogger.GetFor("file").Setup(LogTarget.File);
            Assert.True(LogManager.Configuration.AllTargets.Any(target => target is FileTarget));
        }
    }
}
