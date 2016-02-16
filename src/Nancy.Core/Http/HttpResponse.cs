using System.Diagnostics;
using System.IO;

namespace Nancy.Core.Http
{
    [DebuggerDisplay("{ToString(), nq}")]
    public abstract class HttpResponse
    {
        public abstract HttpContext Context { get; }

        public abstract HttpStatusCode StatusCode { get; set; }

        public abstract string ReasonPhrase { get; set; }

        // TODO: Headers (Specialize Content-Type and Content-Length?)

        public abstract Stream Body { get; set; }

        // TODO: Cookies

        public override string ToString()
        {
            return $"{StatusCode.ToString()} {ReasonPhrase}";
        }
    }
}