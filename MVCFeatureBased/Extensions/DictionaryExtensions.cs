using System.Collections.Generic;

namespace MVCFeatureBased.Web.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue @default = default(TValue))
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
            {
                return value;
            }
            return @default;
        }

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }
    }
}