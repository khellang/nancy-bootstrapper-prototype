namespace Nancy.Owin.Http
{
    using System.Collections.Generic;
    using Nancy.Core.Http;

    internal sealed class OwinUrl : Url
    {
        private readonly HttpRequest request;

        private readonly IDictionary<string, object> environment;

        public OwinUrl(HttpRequest request, IDictionary<string, object> environment)
        {
            this.request = request;
            this.environment = environment;
        }

        public override string Scheme
        {
            get { return this.environment.Get<string>(Constants.RequestScheme); }
            set { this.environment.Set(Constants.RequestScheme, value); }
        }

        // TODO: Get host header from request.
        public override string Host { get; set; }

        public override string PathBase
        {
            get { return this.environment.Get<string>(Constants.RequestPathBase); }
            set { this.environment.Set(Constants.RequestPathBase, value); }
        }

        public override string Path
        {
            get { return this.environment.Get<string>(Constants.RequestPath); }
            set { this.environment.Set(Constants.RequestPath, value); }
        }

        public override string QueryString
        {
            get { return this.environment.Get<string>(Constants.RequestQueryString); }
            set { this.environment.Set(Constants.RequestQueryString, value); }
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
