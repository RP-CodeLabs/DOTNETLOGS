using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace DOTNET.LOGS.Shared
{
    [Pure]
    public class CustomEnum<T> : CustomEnum where T : CustomEnum<T>
    {
        private static readonly Dictionary<string, T> CustomEnumContainer = new Dictionary<string,T>(); 
        public void Add(string key, string description, T item)
        {
            Key = key;
            Description = description;
            CustomEnumContainer.Add(key.ToLower(), item);
        }

        public static T GetFor(string key)
        {
            EnsureValues();
            return Get(key);
        }

        protected static T Get(string key)
        {
            if (key == null) return null;
            CustomEnumContainer.TryGetValue(key.ToLower(), out T t);
            return t;
        }

        private static void EnsureValues()
        {
            if(CustomEnumContainer.Count != 0) return;
            var fieldInfos = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            fieldInfos[0].GetValue(null);
        }

        public static IEnumerable<T> GetAll()
        {
            EnsureValues();
            return CustomEnumContainer.Values.Distinct();
        }
    }

    public class CustomEnum
    {
        public string Key { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Key;
        }
    }
}