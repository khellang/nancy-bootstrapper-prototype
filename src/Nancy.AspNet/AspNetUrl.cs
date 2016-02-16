using Microsoft.AspNet.Http.Features;
using Nancy.Core.Http;

namespace Nancy.AspNet
{
    internal class AspNetUrl : Url
    {
        public AspNetUrl(IHttpRequestFeature request)
        {
            Request = request;
        }

        public override string Scheme
        {
            get { return Request.Scheme; }
            set { Request.Scheme = value; }
        }

        public override string Host
        {
            // TODO: Keep this as-is?
            get { return Request.Headers["Host"]; }
            set { Request.Headers["Host"] = value; }
        }

        public override string PathBase
        {
            get { return Request.PathBase; }
            set { Request.PathBase = value; }
        }

        public override string Path
        {
            get { return Request.Path; }
            set { Request.Path = value; }
        }

        public override string QueryString
        {
            get { return Request.QueryString; }
            set { Request.QueryString = value; }
        }

        private IHttpRequestFeature Request { get; }
    }
}