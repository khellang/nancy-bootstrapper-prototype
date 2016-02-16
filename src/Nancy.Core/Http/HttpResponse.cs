using System.IO;

namespace Nancy.Core.Http
{
    public abstract class HttpResponse
    {
        public abstract HttpContext Context { get; }

        public abstract HttpStatusCode StatusCode { get; set; }

        // TODO: Headers (Specialize Content-Type and Content-Length?)

        public abstract Stream Body { get; set; }

        // TODO: Cookies
    }
}