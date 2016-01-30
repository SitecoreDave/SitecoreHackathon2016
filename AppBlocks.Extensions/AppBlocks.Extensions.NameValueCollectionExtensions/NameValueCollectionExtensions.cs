using System;
using System.Collections.Specialized;
using System.Text;

namespace AppBlocks.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static T ChangeValueType<T>(this NameValueCollection @this, string key)
        {
            if (@this[key] == null) return default(T);
            var results = @this[key].ChangeType<T>();
            return results;
        }

        public static string ToKeyValueString(this NameValueCollection @this, string valueSeparator = "=", string keySeparator = "\r\n")
        {
            var results = new StringBuilder();

            foreach (var key in @this.AllKeys)
            {
                results.Append($"{key}{valueSeparator}{@this[key]}{keySeparator}");
            }

            return results.ToString();
        }
    }
}
