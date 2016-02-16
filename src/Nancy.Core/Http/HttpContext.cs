using System.Collections.Generic;
using System.Security.Claims;

namespace Nancy.Core.Http
{
    public abstract class HttpContext
    {
        public abstract HttpRequest Request { get; }

        public abstract HttpResponse Response { get; }

        public abstract ClaimsPrincipal User { get; set; }

        public abstract IDictionary<object, object> Items { get; }
    }
}