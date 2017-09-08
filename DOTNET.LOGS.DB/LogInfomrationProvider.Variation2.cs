using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DOTNET.LOGS.DB.Entities;
using System.Linq;
using LinqKit;

namespace DOTNET.LOGS.DB
{
    class LogInformationProvider2
    {
        private async Task<IList<LogData>> GetLogInformation2(LogQuery logQuery)
        {
            if (logQuery == null) return new List<LogData>();
            using (var context = new DotNetLogEntities("name=DotNetLogEntities" + logQuery.Environment))
            {
                return await context.LogDetails.AsExpandable()
                    .Where(Predicate(logQuery))
                    .Select(res => new LogData()
                    {
                        CustomerSessionId = context.LogMains.FirstOrDefault(c => c.LogId == res.LogId).CustomerSessionID,
                        StartDateTime = res.StartDateTime,
                        EndDateTime = res.EndDateTime,
                        CurrentUrl = res.Url,
                        UrlReferrrer = res.UrlReferrer,
                        Exception = res.Exception
                    })
                    .OrderByDescending(o => o.StartDateTime)
                    .ToArrayAsync();
            }
        }

        ExpressionStarter<LogDetail> Predicate(LogQuery logQuery)
        {
            var predicate = PredicateBuilder.New<LogDetail>();
            predicate.And(l => l.Level == logQuery.Level);
            predicate.And(l => l.StartDateTime.CompareTo(logQuery.StartDateTime) >= 0);
            predicate.And(l => l.EndDateTime <= logQuery.EndDateTime);
            predicate.And(l => l.Url.Contains(logQuery.Application));
            return predicate;
        }
    }
}