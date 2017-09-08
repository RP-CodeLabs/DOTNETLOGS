using System;
using System.Collections.Generic;

namespace DOTNET.LOGS.Shared
{
    public sealed class Maybe<T> : IEquatable<Maybe<T>> where T : class
    {
        public static Maybe<T> None = new Maybe<T>(default(T));
        
        public T Value { get; }

        public bool HasValue => Value != null && !Value.Equals(default(T));

        public bool HasNoValue => !HasValue;

        public Maybe(T value) => Value = value;

        public Maybe<TO> Bind<TO>(Func<T, Maybe<TO>> func) where TO : class => 
                                                    Value == null ? Maybe<TO>.None : func(Value);

        public static implicit operator Maybe<T>(T value) => new Maybe<T>(value);

        public static bool operator ==(Maybe<T> maybe, T value) =>
            maybe != null && (!maybe.HasNoValue && maybe.Value.Equals(value));
        
        public static bool operator !=(Maybe<T> maybe, T value) => !(maybe == value);
        
        public bool Equals(Maybe<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) 
                    || EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as Maybe<T>;
            return other != null && Equals(other);
        }

        public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);
        
        public T Unwrap(T defaultValue = default(T)) => HasValue ? Value : defaultValue;

    }
}
