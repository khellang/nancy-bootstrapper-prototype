namespace Nancy.Core.Http
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public class DefaultHttpContext : HttpContext
    {
        public override HttpRequest Request { get; }

        public override HttpResponse Response { get; }

        public override ClaimsPrincipal User { get; set; }

        public override IDictionary<object, object> Items { get; }
    }
}
