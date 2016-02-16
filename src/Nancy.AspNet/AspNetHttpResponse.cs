using System.IO;
using Microsoft.AspNet.Http.Features;
using Nancy.Core.Http;

namespace Nancy.AspNet
{
    internal class AspNetHttpResponse : HttpResponse
    {
        public AspNetHttpResponse(HttpContext context, IHttpResponseFeature response)
        {
            Context = context;
            Response = response;
        }

        public override HttpContext Context { get; }

        public override HttpStatusCode StatusCode
        {
            get { return Response.StatusCode; }
            set { Response.StatusCode = value.Value; }
        }

        public override string ReasonPhrase
        {
            get { return Response.ReasonPhrase; }
            set { Response.ReasonPhrase = value; }
        }

        public override Stream Body
        {
            get { return Response.Body; }
            set { Response.Body = value; }
        }

        private IHttpResponseFeature Response { get; }
    }
}