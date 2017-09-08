using DOTNET.LOGS.Shared;
using NLog;
using NLog.Config;

namespace DOTNET.LOGS.Logging
{
    public class NLogger : CustomEnum<NLogger>
    {
        private LoggingConfiguration CustomLoggingConfiguration { get; }

        public static NLogger MemoryLogger = new NLogger("memory", new ConfigureMemoryLoggingRules().GetLoggingConfiguration());
        public static NLogger FileLogger = new NLogger("file", new ConfigureFileLoggingRules().GetLoggingConfiguration());

        public NLogger(string key, LoggingConfiguration loggingConfiguration)
        {
            CustomLoggingConfiguration = loggingConfiguration;
            Key = key;
            Add(key,"",this);
        }

        public void Setup(LogTarget logTarget)
        {
            LogManager.Configuration = GetFor(logTarget.ToString()).CustomLoggingConfiguration;
        }
    }
}
