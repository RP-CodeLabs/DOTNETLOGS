using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DOTNET.LOGS.DB.Entities;
using DOTNET.LOGS.Logging;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.DB
{
    public class LogInformationProvider5 : ILogInformationProvider
    {
        [LogError(ErrorType.Warn, Error.ArgumentNullOrEmpty)]
        public async Task<IReadOnlyList<LogData>> GetLogInformation(Maybe<LogQuery> logQuery)
        {
            if (logQuery.HasNoValue) return new List<LogData>();
            using (var context = new DotNetLogEntities("name=DotNetLogEntities" + logQuery.Value.Environment))
            {
                return await LogInformation(logQuery, context);
            }
        }

        private async Task<IReadOnlyList<LogData>> LogInformation(Maybe<LogQuery> logQuery, DotNetLogEntities context)
        {
            return await context.LogDetails
                .WhichAreError(logQuery.Value.Level)
                .WhichHaveStartDateTimeGreaterThan(logQuery.Value.StartDateTime)
                .WhichAreLessThanEqualToEndDateTime(logQuery.Value.EndDateTime)
                .WhichContainsAppliationInUrl(logQuery.Value.Application)
                .Select(res => new LogData()
                {
                    CustomerSessionId = context.LogMains.FirstOrDefault(c => c.LogId == res.LogId).CustomerSessionID,
                    StartDateTime = res.StartDateTime,
                    EndDateTime = res.EndDateTime,
                    CurrentUrl = res.Url,
                    UrlReferrrer = res.UrlReferrer,
                    Exception = res.Exception
                }).OrderByDescending(o => o.StartDateTime).ToArrayAsync();
        }
    }
}
