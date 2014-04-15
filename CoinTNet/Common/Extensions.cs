using System;
using System.Collections.Generic;

namespace CoinTNet.Common
{
    /// <summary>
    /// Contains extensions for collections
    /// </summary>
    static class CollectionsExtensions
    {
        /// <summary>
        /// Applies an action to all the elements of a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEachExt<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var o in list)
                action(o);
            return list;
        }
    }

    /// <summary>
    /// Contains extensions for strings
    /// </summary>
    static class StringExtensions
    {
        /// <summary>
        /// Checks i a string is null or empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// Returns the string's value if the string is not null, or an empty string otherwise
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EmptyIfNull(this string s)
        {
            return s == null ? string.Empty : s;
        }

        public static string MaxCharacters(this string s, int maxNbChar)
        {
            return s.EmptyIfNull().Length <= maxNbChar ? s : s.Substring(0, maxNbChar);
        }
    }


    static class DateTimeExtensions
    {

        public static DateTime ChangeTypeToUtc(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, DateTimeKind.Utc);
            //return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        }



    }


}
