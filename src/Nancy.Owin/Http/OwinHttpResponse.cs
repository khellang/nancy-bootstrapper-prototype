namespace Nancy.Owin.Http
{
    using System.Collections.Generic;
    using System.IO;
    using Nancy.Core.Http;

    internal sealed class OwinHttpResponse : HttpResponse
    {
        private readonly IDictionary<string, object> environment;

        public OwinHttpResponse(HttpContext context, IDictionary<string, object> environment)
        {
            this.Context = context;
            this.environment = environment;
        }

        public override HttpContext Context { get; }

        public override HttpStatusCode StatusCode
        {
            get { return this.environment.Get<int>(Constants.ResponseStatusCode); }
            set { this.environment.Set(Constants.ResponseStatusCode, value.Value); }
        }

        public override string ReasonPhrase
        {
            get { return this.environment.Get<string>(Constants.ResponseReasonPhrase); }
            set { this.environment.Set(Constants.ResponseReasonPhrase, value); }
        }

        public override Stream Body
        {
            get { return this.environment.Get<Stream>(Constants.ResponseBody); }
            set { this.environment.Set(Constants.ResponseBody, value); }
        }
    }
}
