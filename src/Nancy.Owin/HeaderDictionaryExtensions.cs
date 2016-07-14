namespace Nancy.Owin
{
    using System.Globalization;
    using Nancy.Core.Http;

    internal static class HeaderDictionaryExtensions
    {
        private const NumberStyles ContentLengthNumberStyle = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite;

        public static long? GetContentLength(this IHeaderDictionary headers)
        {
            return headers.ParseSingleValue<long?>(HttpHeaderNames.ContentLength, value =>
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }

                long parsedValue;
                if (long.TryParse(value, ContentLengthNumberStyle, CultureInfo.InvariantCulture, out parsedValue))
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
