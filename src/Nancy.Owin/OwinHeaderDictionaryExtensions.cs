namespace Nancy.Owin
{
    using System.Collections.Generic;

    internal static class OwinHeaderDictionaryExtensions
    {
        public static string GetSingleValue(this IDictionary<string, string[]> headers, string name)
        {
            string[] values;
            if (!headers.TryGetValue(name, out values))
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
