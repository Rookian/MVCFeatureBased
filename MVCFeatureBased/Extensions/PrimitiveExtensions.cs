using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MVCFeatureBased.Web.Extensions
{
    public static class PrimitiveExtensions
    {

        /// <summary>
        /// Casted den Wert in einen String. Ist der Wert NULL wird ein Leerstring zuückgegeben.
        /// Der Wert muss nicht auf NULL geprüft werden.
        /// </summary>
        /// <param name="value">Wert (kann auch NULL sein)</param>
        /// <returns></returns>
        public static string ToNullSafeString(this object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Casted den übergebenen String in ein Int32. Schlägt das fehl, wird 0 zurückgegeben.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToIntOrZero(this string value)
        {
            int number;
            Int32.TryParse(value, out number);
            return number;
        }



        /// <summary>
        /// Prüft, ob der Wert NULL oder leer ist
        /// </summary>
        /// <param name="value">Wert (kann auch NULL sein)</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object value)
        {
            return string.IsNullOrEmpty(value.ToNullSafeString());
        }

        /// <summary>
        /// Gibt TRUE zurück, wenn der Wert = 1 und FALSE, wenn der Wert gleich 0 ist.
        /// Sonst wird eine "ArgumentException" geworfen.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this int value)
        {
            if (value == 0)
                return false;

            if (value == 1)
                return true;

            throw new ArgumentException("Value ist kein boolescher Wert");
        }

        public static bool EqualsIgnoreCase(this string str, string stringToCompare)
        {
            return str.ToNullSafeString().Equals(stringToCompare, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool EndsWithIgnoreCase(this string str, string value)
        {
            return str.EndsWith(value, true, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Erweitert den String um den übergebenen Wert, getrennt durch das übergebene Trennzeichen. Standardmäßig wird ", " als Trennzeichen verwendet.
        /// Das Trennzeichen kommt nur zur Anwendung, wenn beide Strings nicht leer sind.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Append(this string str, object value, string separator = ", ")
        {
            if (value.IsNullOrEmpty())
                return str;

            if (string.IsNullOrEmpty(str))
                return value.ToString();

            if (separator == null)
                separator = ", ";

            return string.Concat(str, separator, value);
        }

        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        public static bool IsEnumerableType(this Type type)
        {
            return type.GetInterfaces().Contains(typeof(IEnumerable));
        }

        public static bool IsListType(this Type type)
        {
            return type.GetInterfaces().Contains(typeof(IList));
        }

        public static bool IsListOrDictionaryType(this Type type)
        {
            return type.IsListType() || type.IsDictionaryType();
        }

        public static bool IsDictionaryType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                return true;

            var genericInterfaces = type.GetInterfaces().Where(t => t.IsGenericType);
            var baseDefinitions = genericInterfaces.Select(t => t.GetGenericTypeDefinition());
            return baseDefinitions.Any(t => t == typeof(IDictionary<,>));
        }

        public static Type GetDictionaryType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                return type;

            var genericInterfaces = type.GetInterfaces().Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IDictionary<,>));
            return genericInterfaces.FirstOrDefault();
        }

        /// <summary>
        /// Liefert den durch den übergebenen Wert begrenzten Maximalwert.
        /// </summary>
        /// <param name="value">Aktueller Wert. Dieser kann auch über dem Grenzwert liegen.</param>
        /// <param name="maxValue">Grenzwert. Dieser stellt die maximale Grenze dar.</param>
        /// <returns>Ggf- begrenzter Wert auch der Überprüfung der beiden Werte.</returns>
        public static int LimitMax(this int value, int maxValue)
        {
            return value <= maxValue ? value : maxValue;
        }

        public static int CountUpperChars(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? 0 : str.Count(char.IsUpper);
        }

        public static int CountLowerChars(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? 0 : str.Count(char.IsLower);
        }

        public static int CountNumericChars(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? 0 : str.Count(char.IsNumber);
        }

        /// <summary>
        /// Gibt das Listenelement der übergebenen Position zurück. Existiert kein Element an dieser Position wird ein neues Objekt zurückgegeben.
        /// </summary>
        /// <typeparam name="T">Objekt</typeparam>
        /// <param name="list">Liste</param>
        /// <param name="position">Position</param>
        /// <returns></returns>
        public static T ElementAtOrDefaultNew<T>(this IList<T> list, int position) where T : new()
        {
            return list != null && position >= 0 && position < list.Count() ? list[position] : new T();
        }

        public static T FirstElementOrDefaultNew<T>(this IList<T> list) where T : new()
        {
            return list.ElementAtOrDefaultNew(0);
        }

        /// <summary>
        /// Gibt eine Komma-separierten (default) String der Liste zurück. Leere Strings oder NULL-Werte werden ignoriert.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinWithoutEmptyValues<T>(this IEnumerable<T> list, string separator = ", ")
        {
            return list != null
                ? string.Join(!string.IsNullOrEmpty(separator) ? separator : ", ", list.Where(value => !value.IsNullOrEmpty()))
                : null;
        }
    }
}