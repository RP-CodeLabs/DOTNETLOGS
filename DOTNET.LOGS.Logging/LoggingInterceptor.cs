using System;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace DOTNET.LOGS.Logging
{
    public class LoggingInterceptor : IAsyncInterceptor
    {
        public void InterceptSynchronous(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                OnException(invocation, ex);
            }
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            invocation.Proceed();
            var task = (Task) invocation.ReturnValue;
            await task;
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation) 
        {
            try
            {
                invocation.Proceed();
                return await (Task<TResult>) invocation.ReturnValue;
            }
            catch (Exception ex)
            {
                OnException(invocation, ex);
                var attribute = AttributeProvider.GetCustomAttribute<LogErrorAttribute>(invocation.MethodInvocationTarget ?? invocation.Method);
                //ExceptionType.GetFor(ex.GetType().Name).ExceptionHandler(ex, attribute.LogLevel);
                ExceptionType.GetFor(attribute.Error.ToString()).ExceptionHandler(ex, attribute.LogLevel);
                return default(TResult);
            }
        }

        private static void OnException(IInvocation invocation, Exception ex)
        {
            var sb = new StringBuilder();
            sb.Append($"Class name :{invocation.GetType().Name}; Method name : {invocation.Method.Name}");
            var parameters = invocation.Method.GetParameters();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                sb.AppendFormat("{0}={1}", parameters[i].Name, invocation.Arguments[i]);
            }
            sb.AppendFormat(") {0} caught: {1})", ex.GetType().Name, ex.Message);
            NLogger.GetFor("memory").Setup(LogTarget.Memory);
            NLogLogging.GetFor("Error").Log(sb.ToString());
        }
    }
}
