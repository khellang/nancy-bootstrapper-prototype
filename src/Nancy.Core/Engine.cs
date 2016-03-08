namespace Nancy.Core
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core.Http;

    public class Engine : IEngine
    {
        public async Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            Check.NotNull(context, nameof(context));

            context.Response.StatusCode = HttpStatusCode.Ok;
            context.Response.ContentType = MediaRange.ApplicationJson;

            using (var writer = new StreamWriter(context.Response.Body))
            {
                await writer.WriteLineAsync($@"{{ ""url"": ""{context.Request.Url}"" }}");
                await writer.FlushAsync();
            }
        }
    }
}
