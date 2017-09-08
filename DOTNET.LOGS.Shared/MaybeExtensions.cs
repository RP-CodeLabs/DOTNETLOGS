namespace DOTNET.LOGS.Shared
{
    public static class MaybeExtensions
    {
        public static Maybe<T> Return<T>(this T value) where T : class
        {
            return value == null ? Maybe<T>.None : new Maybe<T>(value);
        }
    }
}
