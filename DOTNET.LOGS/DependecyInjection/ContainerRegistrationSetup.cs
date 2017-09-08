using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

namespace DOTNET.LOGS.DependecyInjection
{
    public class ContainerRegistrationSetup
    {
        //public static readonly string Namespace = "DOTNET.LOGS,DOTNET.LOGS.DB,DOTNET.LOGS.Logging" ;

        public void Register()
        {
            Register("DOTNET.LOGS");
            Register("DOTNET.LOGS.DB");
            Register("DOTNET.LOGS.Logging");
        }

        private void Register(string nameSpace)
        {
            foreach (var depRegistrar in GetListOfType<DependencyRegistrar>(nameSpace))
            {
                var registrar = Activator.CreateInstance(depRegistrar) as DependencyRegistrar;
                registrar?.Register();
            }
        }

        private IEnumerable<Type> GetListOfType<TDependencyToRegister>(string nameSpace) where TDependencyToRegister : class
        {
            var allReferencedAssemblies = BuildManager.GetReferencedAssemblies();
            foreach (Assembly assembly in allReferencedAssemblies)
            {
                if (assembly != null && assembly.FullName.StartsWith(nameSpace))
                {
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    yield return assembly.GetExportedTypes().Where(type => type.GetInterfaces().Contains(typeof(TDependencyToRegister))) as Type;
                }
            }
        }
    }
}