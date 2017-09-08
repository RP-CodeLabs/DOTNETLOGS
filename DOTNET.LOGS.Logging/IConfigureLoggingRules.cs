using NLog.Config;

namespace DOTNET.LOGS.Logging
{
    public interface IConfigureLoggingRules
    {
        LoggingConfiguration GetLoggingConfiguration();
    }
}