using System;
using System.Reflection;

namespace DOTNET.LOGS.Logging
{
    public class AttributeProvider
    {
        public static T GetCustomAttribute<T>(MethodInfo methodInfo) where T : Attribute
        {
            return (T)Attribute.GetCustomAttribute(methodInfo, typeof(T));
        }
    }
}
