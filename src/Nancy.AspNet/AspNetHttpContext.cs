namespace Nancy.AspNet
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using Microsoft.AspNet.Http.Features;
    using Nancy.Core.Http;

    internal class AspNetHttpContext : HttpContext
    {
        private readonly Microsoft.AspNet.Http.HttpContext context;

        public AspNetHttpContext(Microsoft.AspNet.Http.HttpContext context)
        {
            this.context = context;
            this.Request = new AspNetHttpRequest(this, context.Features.Get<IHttpRequestFeature>());
            this.Response = new AspNetHttpResponse(this, context.Features.Get<IHttpResponseFeature>());
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
