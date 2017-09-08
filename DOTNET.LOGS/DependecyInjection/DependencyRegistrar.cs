using System.Collections.Generic;
using SimpleInjector;

namespace DOTNET.LOGS.DependecyInjection
{
    public abstract class DependencyRegistrar
    {
        public abstract List<DependencyRegistartionMap> SetDependencyMap();

        internal void Register()
        {
            var dependencyMap = SetDependencyMap();
            dependencyMap.ForEach(dependency => new Container().Register(dependency.InterfaceType, dependency.ConcreteType, dependency.Scope));
        }
    }
}