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

        public override string Host
        {
            get { return this.request.Headers.GetSingleValue(HttpHeaderNames.Host); }
            set { this.request.Headers.SetSingleValue(HttpHeaderNames.Host, value); }
        }

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
            get { return GetQueryString(this.environment); }
            set { SetQueryString(this.environment, value); }
        }

        public void CopyFrom(Url url)
        {
            this.Scheme = url.Scheme;
            this.Host = url.Host;
            this.PathBase = url.PathBase;
            this.Path = url.Path;
            this.QueryString = url.QueryString;
        }

        private static string GetQueryString(IDictionary<string, object> environment)
        {
            var queryString = environment.Get<string>(Constants.RequestQueryString);

            if (!string.IsNullOrEmpty(queryString))
            {
                // We expect the query string to start with a '?'.
                return string.Concat('?', queryString);
            }

            return queryString;
        }

        private static void SetQueryString(IDictionary<string, object> environment, string queryString)
        {
            if (!string.IsNullOrEmpty(queryString))
            {
                if (queryString[0] == '?')
                {
                    // We don't want to store the query string with a leading '?' in OWIN.
                    queryString = queryString.Substring(1);
                }
            }

            environment.Set(Constants.RequestQueryString, queryString);
        }
    }
}
