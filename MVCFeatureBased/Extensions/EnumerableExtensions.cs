using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCFeatureBased.Web.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null) return;

            foreach (var o in collection)
            {
                action(o);
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }
    }
}