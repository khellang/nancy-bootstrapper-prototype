namespace Nancy.AspNet.Http
{
    using Microsoft.AspNet.Http;
    using Nancy.Core.Http;

    internal sealed class AspNetUrl : Url
    {
        private readonly Microsoft.AspNet.Http.HttpRequest request;

        public AspNetUrl(Microsoft.AspNet.Http.HttpRequest request)
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
            get { return this.request.Host.Value; }
            set { this.request.Host = new HostString(value); }
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
            get { return this.request.QueryString.Value; }
            set { this.request.QueryString = new QueryString(value); }
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
