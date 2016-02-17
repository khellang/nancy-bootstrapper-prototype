namespace Nancy.Core.Http
{
    using System.Diagnostics;
    using System.IO;

    [DebuggerDisplay("{ToString(), nq}")]
    public abstract class HttpRequest
    {
        public abstract HttpContext Context { get; }

        public abstract HttpMethod Method { get; set; }

        public abstract Url Url { get; set; }

        public abstract string Protocol { get; set; }

        // TODO: Headers (Specialize Content-Type and Content-Length?)

        public abstract Stream Body { get; set; }

        // TODO: Cookies

        // TODO: Form

        public override string ToString()
        {
            return $"{this.Method.ToString()} {this.Url} {this.Protocol}";
        }
    }
}
