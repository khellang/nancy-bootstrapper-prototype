namespace Nancy.Owin.Http
{
    using System.Globalization;
    using Nancy.Core.Http;

    internal static class HeaderDictionaryExtensions
    {
        public static long? GetContentLength(this IHeaderDictionary headers)
        {
            return headers.ParseSingleValue<long?>(HttpHeaderNames.ContentLength, value =>
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }

                const NumberStyles styles = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite;

                long parsedValue;
                if (long.TryParse(value, styles, CultureInfo.InvariantCulture, out parsedValue))
                {
                    return parsedValue;
                }

                return null;
            });
        }

        public static void SetContentLength(this IHeaderDictionary headers, long? value)
        {
            if (value.HasValue)
            {
                headers.SetSingleValue(HttpHeaderNames.ContentLength, value.Value.ToString(CultureInfo.InvariantCulture));
                return;
            }

            headers.Remove(HttpHeaderNames.ContentLength);
        }
    }
}
