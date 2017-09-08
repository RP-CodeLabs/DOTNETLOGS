using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DOTNET.LOGS.DB.Entities;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.DB
{
    class LogInformationProvider7
    {
        public async Task<IReadOnlyList<LogData>> GetLogInformation(Maybe<LogQuery> logQuery)
        {
            if (logQuery.HasNoValue) return new List<LogData>();
            return await DisposableAsync.Using(() => new DotNetLogEntities("name=DotNetLogEntities" + logQuery.Value.Environment),
                context => LogInformation(logQuery, context));
        }

        private async Task<IReadOnlyList<LogData>> LogInformation(Maybe<LogQuery> logQuery, DotNetLogEntities context)
            => await context.LogDetails
                .Where(new ErrorSpecification(logQuery.Value.Level).ToExpression())
                .Where(new StartDateTimeSpecification(DateTime.Parse(logQuery.Value.StartDateTime)).ToExpression())
                .Where(new EndDateTimeSpecification(logQuery.Value.EndDateTime).ToExpression())
                .Where(new ApplicationSpecification(logQuery.Value.Application).ToExpression())
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

    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfied(T entity) => ToExpression().Compile().Invoke(entity);

        public Specification<T> And(Specification<T> specification) => new AndSpecification<T>(this, specification);

        public Specification<T> Or(Specification<T> specification) => new OrSpecification<T>(this, specification);

        public Specification<T> Not() => new NotSpecification<T>(this);
    }

    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();
            var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }

    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();
            var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
        }
    }

    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        public NotSpecification(Specification<T> specification) => _specification = specification;

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expression = _specification.ToExpression();
            var notExpression = Expression.Not(expression);
            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }

    public class ErrorSpecification : Specification<LogDetail>
    {
        private readonly string _level;

        public ErrorSpecification(string level) => _level = level; 

        public override Expression<Func<LogDetail, bool>> ToExpression() => logDetail => logDetail.Level == _level;
    }

    public class StartDateTimeSpecification : Specification<LogDetail>
    {
        private readonly DateTime _startDateTime;

        public StartDateTimeSpecification(DateTime startDateTime) => _startDateTime = startDateTime;

        public override Expression<Func<LogDetail, bool>> ToExpression() => logDetail => DateTime.Parse(logDetail.StartDateTime) >= _startDateTime;
    }

    public class EndDateTimeSpecification : Specification<LogDetail>
    {
        private readonly DateTime _endDateTime;

        public EndDateTimeSpecification(DateTime endDateTime)
        {
            _endDateTime = endDateTime;
        }

        public override Expression<Func<LogDetail, bool>> ToExpression() => logDetail => logDetail.EndDateTime <= _endDateTime;
    }

    public class ApplicationSpecification : Specification<LogDetail>
    {
        private readonly string _application;

        public ApplicationSpecification(string application) => _application = application;

        public override Expression<Func<LogDetail, bool>> ToExpression() => logDetail => logDetail.Url.Contains(_application);
    }
}

