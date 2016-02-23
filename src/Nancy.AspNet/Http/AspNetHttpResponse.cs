namespace Nancy.AspNet.Http
{
    using System.IO;
    using Microsoft.AspNet.Http.Features;
    using Nancy.Core.Http;

    internal sealed class AspNetHttpResponse : HttpResponse
    {
        private readonly IHttpResponseFeature response;

        public AspNetHttpResponse(HttpContext context, IHttpResponseFeature response)
        {
            this.Context = context;
            this.response = response;
        }

        public override HttpContext Context { get; }

        public override HttpStatusCode StatusCode
        {
            get { return this.response.StatusCode; }
            set { this.response.StatusCode = value.Value; }
        }

        public override string ReasonPhrase
        {
            get { return this.response.ReasonPhrase; }
            set { this.response.ReasonPhrase = value; }
        }

        public override Stream Body
        {
            get { return this.response.Body; }
            set { this.response.Body = value; }
        }
    }
}
