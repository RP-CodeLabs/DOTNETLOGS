using System;
using System.Linq;

namespace DOTNET.LOGS.DB
{
    public static class LogDetailPredicates
    {
        public static IQueryable<LogDetail> WhichAreError(this IQueryable<LogDetail> logDetail, string level)
        {
            return logDetail.Where(l => l.Level == level);
        }

        public static IQueryable<LogDetail> WhichHaveStartDateTimeGreaterThan(this IQueryable<LogDetail> logDetails, string startDateTime)
        {
            return logDetails.Where(l => string.Compare(l.StartDateTime, startDateTime, StringComparison.Ordinal) >= 0);
        }

        public static IQueryable<LogDetail> WhichAreLessThanEqualToEndDateTime(this IQueryable<LogDetail> logDetails, DateTime endDateTime)
        {
            return logDetails.Where(l => l.EndDateTime <= endDateTime);
        }

        public static IQueryable<LogDetail> WhichContainsAppliationInUrl(this IQueryable<LogDetail> logDetails, string application)
        {
            return logDetails.Where(l => l.Url.Contains(application));
        }
    }
}
