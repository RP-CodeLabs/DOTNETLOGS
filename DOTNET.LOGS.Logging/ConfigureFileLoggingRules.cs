using NLog;
using NLog.Config;
using NLog.Targets;

namespace DOTNET.LOGS.Logging
{
    internal class ConfigureFileLoggingRules : IConfigureLoggingRules
    {
        public LoggingConfiguration GetLoggingConfiguration()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget()
            {
                FileName = "${basedir}/App_Data/logging/${shortdate}.log",
                Layout = "${longdate} ${uppercase:${level}} ${message}"
            };
            config.AddTarget("file", fileTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            return config;
        }
    }
}