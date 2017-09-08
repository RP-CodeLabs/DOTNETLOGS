using System;
using Castle.DynamicProxy;

namespace DOTNET.LOGS
{
    public class Interceptor
    {
        private static readonly ProxyGenerator Generator = new ProxyGenerator();

        public static object CreateProxy(Type type, IAsyncInterceptor interceptor, object target) => Generator.CreateInterfaceProxyWithTarget(type, target, interceptor);
        
    }
}