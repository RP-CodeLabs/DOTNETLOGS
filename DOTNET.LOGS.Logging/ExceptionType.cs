using System;
using DOTNET.LOGS.Shared;

namespace DOTNET.LOGS.Logging
{
    public class ExceptionType : CustomEnum<ExceptionType>
    {
        public Action<Exception, ErrorType> ExceptionHandler { get; set; }

        public static ExceptionType Null = new ExceptionType("ArgumentNullOrEmpty", (exception, errorType) => new ExceptionLogger().ArgumentNullOrEmpty(exception, errorType));

        private ExceptionType(string key, Action<Exception, ErrorType> exceptionHandler)
        {
            ExceptionHandler = exceptionHandler;
            Key = key;
            Add(Key,"", this);
        }
    }

    public class ExceptionLogger
    {
        public void ArgumentNullOrEmpty(Exception nullReferenceException, ErrorType errorType)
        {
            throw new NotImplementedException();
        }
    }
}
