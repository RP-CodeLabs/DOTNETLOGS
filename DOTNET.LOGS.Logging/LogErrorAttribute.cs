using System;

namespace DOTNET.LOGS.Logging
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LogErrorAttribute : Attribute
    {
        public LogErrorAttribute(ErrorType logLevel, Error error)
        {
            LogLevel = logLevel;
            Error = error;
        }

        public ErrorType LogLevel { get; }
        public Error Error { get; }
    }

    public enum ErrorType
    {
        Fatal,
        Warn
    }

    public enum Error
    {
        ArgumentNullOrEmpty,
        ServiceError,
        Start,
        Stop
    }
}