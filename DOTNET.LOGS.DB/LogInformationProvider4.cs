using System.Collections.Generic;
using System.Linq;
using DOTNET.LOGS.DB.BusinessRules;
using DOTNET.LOGS.DB.Entities;

namespace DOTNET.LOGS.DB
{
    class LogInformationProvider4
    {
        public IList<LogData> GetLogInformation(LogQuery logQuery)
        {
            if (logQuery == null) return new List<LogData>();
            using (var context = new DotNetLogEntities("name=DotNetLogEntities" + logQuery.Environment))
            {
                return new Filter(context).LogDetails()
                    .For().Error(logQuery.Level).And()
                    .StartDateTime().NotNull(logQuery.StartDateTime).AndAlso().GreaterThanOrEqual(logQuery.StartDateTime)
                    .With().EndDateTime().LessThanOrEqual(logQuery.EndDateTime).Contains(logQuery.Application)
                    .Select(res => new LogData()
                    {
                        CustomerSessionId = context.LogMains.FirstOrDefault(c => c.LogId == res.LogId)?.CustomerSessionID,
                        StartDateTime = res.StartDateTime,
                        EndDateTime = res.EndDateTime,
                        CurrentUrl = res.Url,
                        UrlReferrrer = res.UrlReferrer,
                        Exception = res.Exception
                    }).OrderByDescending(o => o.StartDateTime).ToList();
            }
        }
    }
}
