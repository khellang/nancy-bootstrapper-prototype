using System.IO;
using Microsoft.AspNet.Http.Features;
using Nancy.Core.Http;

namespace Nancy.AspNet
{
    internal class AspNetHttpRequest : HttpRequest
    {
        public AspNetHttpRequest(HttpContext context, IHttpRequestFeature request)
        {
            Context = context;
            Request = request;
            AspNetUrl = new AspNetUrl(request);
        }

        public override HttpContext Context { get; }

        public override HttpMethod Method
        {
            get { return Request.Method; }
            set { Request.Method = value.Value; }
        }

        public override Url Url
        {
            get { return AspNetUrl; }
            set { SetUrl(AspNetUrl, value); }
        }

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

        private IHttpRequestFeature Request { get; }

        private AspNetUrl AspNetUrl { get; }

        private static void SetUrl(Url url, Url newUrl)
        {
            // If the user sets a new URL, we need to copy over the values.

            url.Scheme = newUrl.Scheme;
            url.Host = newUrl.Host;
            url.PathBase = newUrl.PathBase;
            url.Path = newUrl.Path;
            url.QueryString = newUrl.QueryString;
        }
    }
}