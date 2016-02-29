namespace Nancy.AspNet.Http
{
    using System.IO;
    using Microsoft.AspNet.Http.Features;
    using Nancy.Core.Http;

    internal sealed class AspNetHttpResponse : HttpResponse
    {
        private readonly Microsoft.AspNet.Http.HttpResponse response;

        private readonly IHttpResponseFeature responseFeature;

        public AspNetHttpResponse(HttpContext context, Microsoft.AspNet.Http.HttpResponse response)
        {
            this.Context = context;
            this.response = response;
            this.responseFeature = response.HttpContext.Features.Get<IHttpResponseFeature>();
            this.Headers = new AspNetHeaderDictionary(response.Headers);
        }

        public override HttpContext Context { get; }

        public override HttpStatusCode StatusCode
        {
            get { return this.response.StatusCode; }
            set { this.response.StatusCode = value.Value; }
        }

        public override string ReasonPhrase
        {
            get { return this.responseFeature.ReasonPhrase; }
            set { this.responseFeature.ReasonPhrase = value; }
        }

        public override IHeaderDictionary Headers { get; }

        public override long? ContentLength
        {
            get { return this.response.ContentLength; }
            set { this.response.ContentLength = value; }
        }

        public override string ContentType
        {
            get { return this.response.ContentType; }
            set { this.response.ContentType = value; }
        }

        public override Stream Body
        {
            get { return this.response.Body; }
            set { this.response.Body = value; }
        }
    }
}
