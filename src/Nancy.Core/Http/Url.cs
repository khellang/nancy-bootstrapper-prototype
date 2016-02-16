using System;

namespace Nancy.Core.Http
{
    public abstract class Url
    {
        public const string HttpScheme = "http";

        public const string HttpsScheme = "https";

        public abstract string Scheme { get; set; }

        public abstract string Host { get; set; }

        public abstract string PathBase { get; set; }

        public abstract string Path { get; set; }

        public abstract string QueryString { get; set; }

        public bool IsHttps => string.Equals(Scheme, HttpsScheme, StringComparison.OrdinalIgnoreCase);
    }
}