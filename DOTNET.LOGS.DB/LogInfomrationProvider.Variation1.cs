using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DOTNET.LOGS.DB.Entities;
using System.Linq;

namespace DOTNET.LOGS.DB
{
    class LogInformationProvider1 
    {
        async Task<IList<LogData>> GetLogInformation1(LogQuery logQuery)
        {
            if (logQuery == null) return new List<LogData>();
            using (var context = new DotNetLogEntities("name=DotNetLogEntities" + logQuery.Environment))
            {
                var result = await context.LogDetails
                    .Where(l => l.Level == logQuery.Level)
                    .Where(s => !string.IsNullOrEmpty(s.StartDateTime) &&
                                s.StartDateTime.CompareTo(logQuery.StartDateTime) >= 0)
                    .Where(e => e.EndDateTime != null && e.EndDateTime <= logQuery.EndDateTime)
                    .Where(u => u.Url.Contains(logQuery.Application))
                    .ToArrayAsync();
                return result.Select(r => new LogData()
                {
                    CustomerSessionId = context.LogMains.FirstOrDefault(c => c.LogId == r.LogId).CustomerSessionID,
                    StartDateTime = r.StartDateTime,
                    EndDateTime = r.EndDateTime,
                    CurrentUrl = r.Url,
                    UrlReferrrer = r.UrlReferrer,
                    Exception = r.Exception
                })
                    .OrderByDescending(o => o.StartDateTime)
                    .ToList();
            }
        }
    }
}