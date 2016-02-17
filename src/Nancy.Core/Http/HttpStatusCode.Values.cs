namespace Nancy.Core.Http
{
    public partial struct HttpStatusCode
    {
        public static HttpStatusCode Ok { get; } = 200;

        public static HttpStatusCode Created { get; } = 201;

        public static HttpStatusCode Accepted { get; } = 202;

        public static HttpStatusCode NonAuthoritativeInformation { get; } = 203;

        public static HttpStatusCode NoContent { get; } = 204;

        // TODO: Generate these...
    }
}
