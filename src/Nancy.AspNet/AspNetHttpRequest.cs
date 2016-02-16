using System.IO;
using Microsoft.AspNet.Http.Features;
using Nancy.Core.Http;
using HttpContext = Nancy.Core.Http.HttpContext;
using HttpRequest = Nancy.Core.Http.HttpRequest;

namespace Nancy.AspNet
{
    internal class AspNetHttpRequest : HttpRequest
    {
        public AspNetHttpRequest(HttpContext context, IHttpRequestFeature request)
        {
            Context = context;
            Request = request;
            Url = new AspNetUrl(request);
        }

        public override HttpContext Context { get; }

        private IHttpRequestFeature Request { get; }

        public override HttpMethod Method
        {
            get { return Request.Method; }
            set { Request.Method = value.Value; }
        }

        public override Url Url { get; }

        public override string Protocol
        {
            get { return Request.Protocol; }
            set { Request.Protocol = value; }
        }

        public override Stream Body
        {
            get { return Request.Body; }
            set { Request.Body = value; }
        }
    }
}