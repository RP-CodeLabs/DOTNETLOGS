using System;

namespace DOTNET.LOGS.Shared
{
    public static class MapExtension
    {
        public static TResult Map<TSource, TResult>(this TSource @this, Func<TSource, TResult> func) 
            => func(@this);
    }
}