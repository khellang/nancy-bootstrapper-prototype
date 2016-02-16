using System;
using System.Diagnostics;
using System.Text;

namespace Nancy.Core.Http
{
    [DebuggerDisplay("{ToString(), nq}")]
    public abstract class Url
    {
        public const string HttpScheme = "http";

        public const string HttpsScheme = "https";

        public const string SchemeDelimiter = "://";

        public abstract string Scheme { get; set; }

        public abstract string Host { get; set; }

        public abstract string PathBase { get; set; }

        public abstract string Path { get; set; }

        public abstract string QueryString { get; set; }

        public bool IsHttps => string.Equals(Scheme, HttpsScheme, StringComparison.OrdinalIgnoreCase);

        public override string ToString()
        {
            var scheme = Scheme;
            var host = Host;
            var pathBase = PathBase;
            var path = Path;
            var queryString = QueryString;

            // PERF: Pre-compute the length to allocate correctly in StringBuilder.
            var length = scheme.Length
                + SchemeDelimiter.Length
                + host.Length
                + pathBase.Length
                + path.Length
                + queryString.Length;

            return new StringBuilder(capacity: length)
                .Append(scheme)
                .Append(SchemeDelimiter)
                .Append(host)
                .Append(pathBase)
                .Append(path)
                .Append(queryString)
                .ToString();
        }
    }
}