using System;
using System.Threading.Tasks;

namespace DOTNET.LOGS.Shared
{
    public class DisposableAsync 
    {
        public static async Task<TResult> Using<TDisposable, TResult>(Func<TDisposable> factory, Func<TDisposable, Task<TResult>> func) where TDisposable : IDisposable
        {
            using (var disposable = factory())
            { 
                return await func(disposable);
            }
        }
    }
}
