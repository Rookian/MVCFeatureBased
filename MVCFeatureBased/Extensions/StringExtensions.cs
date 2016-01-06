using System;

namespace MVCFeatureBased.Web.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsIgnoreCase(this string str, string value)
        {
            return str.IndexOf(value, StringComparison.CurrentCultureIgnoreCase) > -1;
        }

        public static string ToEmptySafeJsonArray(this string str)
        {
            return string.IsNullOrEmpty(str) ? "[]" : str;
        }
    }
}