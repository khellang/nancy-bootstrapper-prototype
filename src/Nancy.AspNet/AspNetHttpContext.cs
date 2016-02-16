using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNet.Http.Features;
using Nancy.Core.Http;

namespace Nancy.AspNet
{
    internal class AspNetHttpContext : HttpContext
    {
        public AspNetHttpContext(Microsoft.AspNet.Http.HttpContext context)
        {
            Context = context;
            Request = new AspNetHttpRequest(this, context.Features.Get<IHttpRequestFeature>());
            Response = new AspNetHttpResponse(this, context.Features.Get<IHttpResponseFeature>());
        }

        private Microsoft.AspNet.Http.HttpContext Context { get; }

        public override HttpRequest Request { get; }

        public override HttpResponse Response { get; }

        public override ClaimsPrincipal User
        {
            get { return Context.User; }
            set { Context.User = value; }
        }

        public override IDictionary<object, object> Items
        {
            get { return Context.Items; }
        }
    }
}