namespace Nancy.AspNet.Http
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Nancy.Core.Http;

    internal sealed class AspNetHttpContext : HttpContext
    {
        private readonly Microsoft.AspNet.Http.HttpContext context;

        public AspNetHttpContext(Microsoft.AspNet.Http.HttpContext context)
        {
            this.context = context;
            this.Request = new AspNetHttpRequest(this, context.Request);
            this.Response = new AspNetHttpResponse(this, context.Response);
        }

        public override HttpRequest Request { get; }

        public override HttpResponse Response { get; }

        public override ClaimsPrincipal User
        {
            get { return this.context.User; }
            set { this.context.User = value; }
        }

        public override IDictionary<object, object> Items => this.context.Items;
    }
}
