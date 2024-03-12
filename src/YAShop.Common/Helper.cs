using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAShop.Common
{
    public static class Helper
    {
        public static T? ToSubj<T>(this string json) => JsonConvert.DeserializeObject<T>(json);

        public static string? ToMinJSON<T>(this T? subj, bool typeNameHandle = false)
        {
            if (subj == null) return null;
            var settings = new JsonSerializerSettings
            { DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
            if (typeNameHandle) settings.TypeNameHandling = TypeNameHandling.Objects;
            var minJSON = JsonConvert.SerializeObject(subj, Formatting.None, settings);
            return minJSON;
        }

        public static string? ToReadableJSON<T>(this T subj, bool withTypeName = false, bool ignoreDefaults = true)
        {
            string? minJSON;
            if (withTypeName)
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                };

                if (ignoreDefaults)
                {
                    settings.DefaultValueHandling = DefaultValueHandling.Ignore;
                    settings.NullValueHandling = NullValueHandling.Ignore;
                }


                settings.Converters.Add(new StringEnumConverter());
                minJSON = subj == null
                    ? null
                    : JsonConvert.SerializeObject(subj, Formatting.Indented, settings);
            }
            else
            {
                var settings = new JsonSerializerSettings();
                if (ignoreDefaults)
                {
                    settings.DefaultValueHandling = DefaultValueHandling.Ignore;
                    settings.NullValueHandling = NullValueHandling.Ignore;
                }
                settings.Converters.Add(new StringEnumConverter());
                minJSON = subj == null
                    ? null
                    : JsonConvert.SerializeObject(subj, Formatting.Indented, settings);
            }

            return minJSON;
        }

        public static string? ToJSONWithTypeName<T>(this T subj)
        {
            var minJSON = subj == null
                ? null
                : JsonConvert.SerializeObject(subj, Formatting.None, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
                });
            return minJSON == "{}" || minJSON == "[]" ? null : minJSON;
        }

        public static T[][] Slice<T>(this IEnumerable<T> items, int size)
        {
            var i = 0;
            return items.GroupBy(_ => i++ / size).Select(g => g.ToArray()).ToArray();
        }

        public static bool NotEmptyList<T>(this T[]? array)
        {
            return array != null && array.Length > 0;
        }

        public static T? GetFirst<T>(this T[]? array)
        {
            return array is { Length: > 0 } ? array[0] : default;
        }

        public static bool IsNullOrEmptyList<T>(this T[]? array)
        {
            return array == null || array.Length == 0;
        }
  
        public static bool IsNotNullOrEmpty(this string? str) => !IsNullOrEmpty(str);
        public static bool IsNullOrEmpty(this string? str)
        {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }
        public static bool IsEmptyObject<T>(this T subj)
        {
            if (subj == null) return true;
            var str = JsonConvert.SerializeObject(subj, Formatting.None,
                new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
            return string.IsNullOrWhiteSpace(str) || str == "{}";
        }

        public static bool IsNullOrEmptyList<T>(this List<T>? list)
        {
            return list == null || list.Count == 0;
        }

        public static bool NotEmptyList<T>(this IEnumerable<T>? list)
        {
            return list != null && list.Any();
        }

        public static List<T> ToListSafe<T>(this T[]? array)
        {
            return array == null || array.Length == 0 ? new List<T>() : array.ToList();
        }

        public static List<T>? ToListSafe<T>(this IEnumerable<T>? list)
        {
            return list?.ToList();
        }
        public static T[]? ToArraySafe<T>(this IEnumerable<T>? list)
        {
            return list?.ToArray();
        }

        public static string ToStringSafe(this decimal? d)
        {
            return d == null ? "" : d.Value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToStringSafe(this int? d)
        {
            return d == null ? "" : d.Value.ToString();
        }

        public static decimal? ToNullableDecimal(this string s)
        {
            return !decimal.TryParse(s, out var d) ? null : d;
        }

        public static decimal ToDecimal(this string s)
        {
            return !decimal.TryParse(s, CultureInfo.InvariantCulture, out var d ) ? 0 : d;
        }

        public static decimal? ToDecimalMeterNullable(this string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            decimal div = 1m;
            if (s.EndsWith("cm"))
            {
                s = s.Replace("cm", "");
                div = 100m;
            }
            else if (s.EndsWith("mm"))
            {
                s = s.Replace("mm", "");
                div = 1000m;
            }
            return !decimal.TryParse(s, out var d) ? null : d / div;
        }
        public static decimal? ToDecimalKgNullable(this string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            decimal div = 1m;
            if (s.EndsWith("g"))
            {
                s = s.Replace("g", "");
                div = 1000m;
            }
            else if (s.EndsWith("kg"))
            {
                s = s.Replace("kg", "");
            }
            return !decimal.TryParse(s.Trim(), out var d) ? null : d / div;
        }

        public static int? ToNullableInt(this string s)
        {
            return !int.TryParse(s, out var d) ? null : d;
        }
        public static int ToInt(this string s)
        {
            return !int.TryParse(s, out var d) ? 0 : d;
        }
        public static long ToLong(this string s)
        {
            return !long.TryParse(s, out var d) ? 0 : d;
        }

        public static Guid? ToNullableGuid(this string s) => !Guid.TryParse(s, out var d) ? null : d;

        public static Guid ToGuid(this string s) => !Guid.TryParse(s, out var d) ? Guid.Empty : d;

        public static T[] ToArraySafe<T>(this List<T>? list)
        {
            return list == null || list.Count == 0 ? new T[] { } : list.ToArray();
        }
    }
}
