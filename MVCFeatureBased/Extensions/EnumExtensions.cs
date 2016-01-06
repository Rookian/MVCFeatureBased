using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCFeatureBased.Web.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<Enum> GetFlags(this Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }

        public static Dictionary<string, bool> GetDictionary(this Enum input)
        {
            return Enum.GetValues(input.GetType())
                .Cast<Enum>()
                .ToDictionary(value => value.ToString(), input.HasFlag);
        }
    }
}