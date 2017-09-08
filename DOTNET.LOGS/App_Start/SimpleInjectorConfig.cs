using System.Web.Mvc;
using DOTNET.LOGS.DB;
using DOTNET.LOGS.Logging;
using DOTNET.LOGS.Models;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace DOTNET.LOGS
{
    public static class SimpleInjectorConfig
    {
        public static void RegisterComponents()
        {
            var container = new Container();
            container.Register<ILogInformationProvider, LogInformationProvider>();
            container.Register<ILogManager, LogManager>();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            container.InterceptWith<LoggingInterceptor>(type => type.IsInterface && type.Name.EndsWith("Manager"));
            container.InterceptWith<LoggingInterceptor>(type => type.IsInterface && type.Name.EndsWith("Provider"));
        }
    }
}