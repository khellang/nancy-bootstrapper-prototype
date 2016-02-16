namespace Nancy.Core.Http
{
    public partial struct HttpMethod
    {
        public static HttpMethod Get { get; } = "GET";

        public static HttpMethod Put { get; } = "PUT";

        public static HttpMethod Post { get; } = "POST";

        public static HttpMethod Delete { get; } = "DELETE";

        public static HttpMethod Patch { get; } = "PATCH";

        public static HttpMethod Head { get; } = "HEAD";

        public static HttpMethod Options { get; } = "OPTIONS";
    }
}
