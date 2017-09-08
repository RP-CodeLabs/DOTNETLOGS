using NLog;
using NLog.Config;
using NLog.Targets;

namespace DOTNET.LOGS.Logging
{
    public class ConfigureMemoryLoggingRules : IConfigureLoggingRules
    {
        public LoggingConfiguration GetLoggingConfiguration()
        {
            var config = new LoggingConfiguration();
            var memoryTarget = new MemoryTarget()
            {
                Layout = "${longdate} ${uppercase:${level}} ${message}"
            };
            config.AddTarget("memory", memoryTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, memoryTarget));
            return config;
        }
    }
}