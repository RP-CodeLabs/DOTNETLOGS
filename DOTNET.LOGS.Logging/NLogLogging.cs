using System;
using DOTNET.LOGS.Shared;
using NLog;

namespace DOTNET.LOGS.Logging
{
    public class NLogLogging : CustomEnum<NLogLogging>
    {
        private static readonly Lazy<Logger> Logger = new Lazy<Logger>(LogManager.GetCurrentClassLogger);

        public Action<string> Log { get; }

        public static NLogLogging Info = new NLogLogging("Info", (log)=> Logger.Value.Info(log));
        public static NLogLogging Warn = new NLogLogging("Warn", (log) => Logger.Value.Warn(log));
        public static NLogLogging Error = new NLogLogging("Error", (log) => Logger.Value.Error(log));
        public static NLogLogging Fatal = new NLogLogging("Fatal", (log) => Logger.Value.Fatal(log));
        public static NLogLogging Debug = new NLogLogging("Debug", (log) => Logger.Value.Debug(log));

        private NLogLogging(string key, Action<string> log)
        {
            Log = log;
            Key = key;
            Add(Key,"", this);
        }
    }
}
