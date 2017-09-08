using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DOTNET.LOGS.DB.Entities;
using DOTNET.LOGS.Logging;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.DB
{
    public class LogInformationProvider6 : ILogInformationProvider
    {
        [LogError(ErrorType.Warn, Error.ArgumentNullOrEmpty)]
        public async Task<IReadOnlyList<LogData>> GetLogInformation(Maybe<LogQuery> logQuery)
        {
            //if (logQuery.HasNoValue) return new List<LogData>();
            return await DisposableAsync.Using(() => new DotNetLogEntities("name=DotNetLogEntities" + logQuery.Value.Environment),
                context => LogInformation(logQuery, context));
        }

        private async Task<IReadOnlyList<LogData>> LogInformation(Maybe<LogQuery> logQuery, DotNetLogEntities context)
            => await context.LogDetails
                .Map(ld => ld.Where(x=>x.Level == logQuery.Value.Level))
                .Map(ld => ld.Where(x => string.Compare(x.StartDateTime, logQuery.Value.StartDateTime, StringComparison.Ordinal) >= 0))
                .Map(ld => ld.Where(x => x.EndDateTime <= logQuery.Value.EndDateTime))
                .Map(ld => ld.Where(x=>x.Url.Contains(logQuery.Value.Application)))
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
