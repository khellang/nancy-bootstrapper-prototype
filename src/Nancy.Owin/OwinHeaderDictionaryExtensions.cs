namespace Nancy.Owin
{
    using System;
    using System.Collections.Generic;

    internal static class OwinHeaderDictionaryExtensions
    {
        public static T ParseSingleValue<T>(this IDictionary<string, string[]> headers, string name, Func<string, T> parser)
        {
            return parser.Invoke(headers.GetSingleValue(name));
        }

        public static string GetSingleValue(this IDictionary<string, string[]> headers, string name)
        {
            if (!headers.TryGetValue(name, out var values))
            {
                return string.Empty;
            }

            if (values.Length > 0)
            {
                return values[0];
            }

            return string.Empty;
        }

        public static void SetSingleValue(this IDictionary<string, string[]> headers, string name, string value)
        {
            headers[name] = new[] { value };
        }
    }
}
