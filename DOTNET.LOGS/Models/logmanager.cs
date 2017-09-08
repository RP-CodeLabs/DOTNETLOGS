using System.Collections.Generic;
using System.Threading.Tasks;
using DOTNET.LOGS.DB;
using DOTNET.LOGS.DB.Entities;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.Models
{
    public class LogManager : ILogManager
    {
        private readonly ILogInformationProvider _logInformationProvider;
        public LogManager(ILogInformationProvider logIngoInformationProvider)
        {
            _logInformationProvider = logIngoInformationProvider;
        }

        public async Task<IReadOnlyList<LogData>> GetLogData(Maybe<LogFilter> logFilter, string environment)
        {
            if(logFilter.HasNoValue) return new List<LogData>();
            return await _logInformationProvider.GetLogInformation(new LogQuery()
            {
                Application = logFilter.Value.Application,
                StartDateTime = logFilter.Value.StartDateTime.ToShortDateString(),
                EndDateTime = logFilter.Value.EndDateTime,
                Level = "Error",
                Environment = environment
            });
        }
    }
}