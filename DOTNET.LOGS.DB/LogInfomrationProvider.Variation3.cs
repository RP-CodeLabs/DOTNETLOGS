using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DOTNET.LOGS.DB.Entities;

namespace DOTNET.LOGS.DB
{
    class LogInformationProvider3
    {
        async Task<IList<LogData>> GetLogInformation3(LogQuery logQuery)
        {
            if (logQuery == null) return new List<LogData>();
            using (var context = new DotNetLogEntities("name=DotNetLogEntities" + logQuery.Environment))
            {
                var result = await context.LogDetails
                .Where(IsLevelError(logQuery.Level)) 
                //.Where(new GenericSpecification<LogDetail>(IsLevelError(logQuery.Level)).Expression)
                .Where(NotNull())
                .Where(GreaterOrEqual(logQuery.StartDateTime))
                .Where(LessThanOrEqual(logQuery.EndDateTime))
                .ToArrayAsync();
                //context.LogDetails.NotNull<LogDetail,string>(logQuery.Level).GreaterThanOrEqual("",logQuery.StartDateTime);
                return result.Select(r => new LogData()
                {
                    CustomerSessionId = context.LogMains.FirstOrDefault(c => c.LogId == r.LogId).CustomerSessionID,
                    StartDateTime = r.StartDateTime,
                    EndDateTime = r.EndDateTime,
                    CurrentUrl = r.Url,
                    UrlReferrrer = r.UrlReferrer,
                    Exception = r.Exception
                }).OrderByDescending(o => o.StartDateTime)
                    .ToList();
            }
        }

        public class GenericSpecification<T>
        {
            public Expression<Func<T, bool>> Expression { get; }

            public GenericSpecification(Expression<Func<T, bool>> expression)
            {
                Expression = expression;
            }

            public bool IsSatisfied(T entity) => Expression.Compile().Invoke(entity);
        }

        Expression<Func<LogDetail, bool>> NotNull()
        {
            return l => string.IsNullOrEmpty(l.StartDateTime);
        }

        Expression<Func<LogDetail, bool>> GreaterOrEqual(string startDateTime)
        {
            return logDetail => DateTime.Parse(logDetail.StartDateTime) > DateTime.Parse(startDateTime);
        }

        Expression<Func<LogDetail, bool>> LessThanOrEqual(DateTime endDateTime)
        {
            return logDetail => logDetail.EndDateTime < endDateTime;
        }

        Expression<Func<LogDetail, bool>> IsLevelError(string coverLevel)
        {
            return l => l.Level == coverLevel;
        }
    }

    public static class Generic
    {
        public static List<T> NotNull<T, TPropertyType>(this IEnumerable<T> collection, string propertyName)
        {
            var pe = Expression.Parameter(typeof(T), "logdetail");
            var property = Expression.Property(pe, propertyName);
            var expr = Expression.Equal(property, Expression.Constant(null, typeof(TPropertyType)));
            var predicate = Expression.Lambda<Func<T, bool>>(expr, pe);
            return collection.Where(predicate.Compile()).ToList();
        }

        public static List<T> GreaterThanOrEqual<T>(this IEnumerable<T> collection, string propertyName, string constant)
        {
            var pe = Expression.Parameter(typeof(T), "logdetail");
            var property = Expression.Property(pe, propertyName);
            var exprConstant = Expression.Constant(constant, constant.GetType());
            var expr = Expression.GreaterThanOrEqual(property, exprConstant);
            var predicate = Expression.Lambda <Func<T, bool>>(expr, pe);
            return collection.Where(predicate.Compile()).ToList();
        }

        public static List<T> LessThanOrEqual<T>(this IEnumerable<T> collection, string propertName, string constant)
        {
            var pe = Expression.Parameter(typeof(T), "logdetail");
            var property = Expression.Property(pe, propertName);
            var exprConstant = Expression.Constant(property, constant.GetType());
            var expr = Expression.LessThanOrEqual(property, exprConstant);
            var predicate = Expression.Lambda<Func<T,bool>>(expr, pe);
            return collection.Where(predicate.Compile()).ToList();
        }
    }
}