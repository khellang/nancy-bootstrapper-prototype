using System;
using System.Threading;
using System.Threading.Tasks;
using Nancy.Bootstrapper.Prototype.Http;

namespace Nancy.Bootstrapper.Prototype
{
    public class Engine : IEngine
    {
        public async Task<HttpResponse> HandleRequest(HttpRequest request, CancellationToken cancellationToken)
        {
            // Simulate some request handling...
            await Task.Delay(TimeSpan.FromMilliseconds(300), cancellationToken).ConfigureAwait(false);

            return new HttpResponse();
        }
    }
}