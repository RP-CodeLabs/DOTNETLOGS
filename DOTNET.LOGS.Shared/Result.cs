using System;
using System.Collections.Generic;

namespace DOTNET.LOGS.Shared
{
    public class Result
    {
        public Result(bool isSuccess,Dictionary<string, object> error)
        {
            Error = error;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }

        public Dictionary<string, object> Error { get; }

        public bool IsFailure => !IsSuccess;

        public static Result Ok() => new Result(true, new Dictionary<string, object>());
        
        public static Result<TValue> Ok<TValue>(TValue value) => new Result<TValue>(value, true, new Dictionary<string, object>());
        
        public static Result Fail(Dictionary<string, object> error) => new Result(false, error);

        public static Result<TValue> Fail<TValue>(Dictionary<string, object> error) => new Result<TValue>(default(TValue), false, error);
        
    }

    public class Result<TValue> : Result
    {
        public TValue Value { get; }

        public Result(TValue value, bool isSuccess, Dictionary<string, object> error) : base(isSuccess, error) => Value = value;
        
    }

    public static class ResultExtensions
    {
        public static Result<T> ToResult<T>(this Maybe<T> maybe, Dictionary<string, object> error) where T : class
        {
            if (maybe.HasNoValue)
                return Result.Fail<T>(error);
            return Result.Ok(maybe.Value);
        }

        public static Result OnSuccess(this Result result, Func<Result> func) => result.IsFailure ? result : func();
    }
}
