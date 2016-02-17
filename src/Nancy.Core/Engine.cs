namespace Nancy.Core
{
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core.Http;
    using static Nancy.Core.Http.HttpStatusCode;

    public class Engine : IEngine
    {
        public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            var hellWorldBytes = Encoding.UTF8.GetBytes("Hello World!");

            context.Response.StatusCode = Ok;    //
            context.Response.StatusCode = 200;   // Pretty cool, eh?

            // TODO: Set Content-Type and Content-Length.

            return context.Response.Body.WriteAsync(hellWorldBytes, 0, hellWorldBytes.Length, cancellationToken);
        }
    }
}
