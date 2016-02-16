using System.IO;
using Nancy.Core.Http;

namespace Nancy.AspNet
{
    internal class AspNetHttpResponse : HttpResponse
    {
        public AspNetHttpResponse(HttpContext context, Microsoft.AspNet.Http.HttpResponse response)
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

        public override Stream Body
        {
            get { return Response.Body; }
            set { Response.Body = value; }
        }

        private Microsoft.AspNet.Http.HttpResponse Response { get; }
    }
}