using Microsoft.AspNet.Http;
using Nancy.Core.Http;

namespace Nancy.AspNet
{
    internal class AspNetUrl : Url
    {
        public AspNetUrl(Microsoft.AspNet.Http.HttpRequest request)
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
            get { return Request.Host.Value; }
            set { Request.Host = new HostString(value); }
        }

        public override string PathBase
        {
            get { return Request.PathBase.Value; }
            set { Request.PathBase = new PathString(value); }
        }

        public override string Path
        {
            get { return Request.Path.Value; }
            set { Request.Path = new PathString(value); }
        }

        public override string QueryString
        {
            get { return Request.QueryString.Value; }
            set { Request.QueryString = new QueryString(value); }
        }

        private Microsoft.AspNet.Http.HttpRequest Request { get; }
    }
}