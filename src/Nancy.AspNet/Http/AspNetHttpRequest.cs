namespace Nancy.AspNet.Http
{
    using System.IO;
    using Microsoft.AspNet.Http.Features;
    using Nancy.Core.Http;

    internal sealed class AspNetHttpRequest : HttpRequest
    {
        private readonly IHttpRequestFeature request;

        private readonly AspNetUrl aspNetUrl;

        public AspNetHttpRequest(HttpContext context, IHttpRequestFeature request)
        {
            this.Context = context;
            this.request = request;
            this.aspNetUrl = new AspNetUrl(request);
        }

        public override HttpContext Context { get; }

        public override HttpMethod Method
        {
            get { return this.request.Method; }
            set { this.request.Method = value.Value; }
        }

        public override Url Url
        {
            get { return this.aspNetUrl; }
            set { this.aspNetUrl.CopyFrom(value); }
        }

        public override string Protocol
        {
            get { return this.request.Protocol; }
            set { this.request.Protocol = value; }
        }

        public override Stream Body
        {
            get { return this.request.Body; }
            set { this.request.Body = value; }
        }
    }
}
