namespace Nancy.AspNet.Http
{
    using Microsoft.AspNet.Http.Features;
    using Nancy.Core.Http;

    internal sealed class AspNetUrl : Url
    {
        private readonly IHttpRequestFeature request;

        public AspNetUrl(IHttpRequestFeature request)
        {
            this.request = request;
        }

        public override string Scheme
        {
            get { return this.request.Scheme; }
            set { this.request.Scheme = value; }
        }

        public override string Host
        {
            // TODO: Keep this as-is?
            get { return this.request.Headers["Host"]; }
            set { this.request.Headers["Host"] = value; }
        }

        public override string PathBase
        {
            get { return this.request.PathBase; }
            set { this.request.PathBase = value; }
        }

        public override string Path
        {
            get { return this.request.Path; }
            set { this.request.Path = value; }
        }

        public override string QueryString
        {
            get { return this.request.QueryString; }
            set { this.request.QueryString = value; }
        }

        public void CopyFrom(Url url)
        {
            this.Scheme = url.Scheme;
            this.Host = url.Host;
            this.PathBase = url.PathBase;
            this.Path = url.Path;
            this.QueryString = url.QueryString;
        }
    }
}
