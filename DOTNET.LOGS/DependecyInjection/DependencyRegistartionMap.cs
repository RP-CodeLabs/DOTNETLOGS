using System;
using SimpleInjector;

namespace DOTNET.LOGS.DependecyInjection
{
    public class DependencyRegistartionMap
    {
        public DependencyRegistartionMap(Type interfaceType, Type concreteType, Lifestyle scope)
        {
            InterfaceType = interfaceType;
            ConcreteType = concreteType;
            Scope = scope;
        }

        internal Type InterfaceType { get; }

        internal Type ConcreteType { get; }
        internal Lifestyle Scope { get; }
    }
}