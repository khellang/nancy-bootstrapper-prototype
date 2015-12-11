using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication7.Cruft.Http;

namespace ConsoleApplication7.Cruft
{
    internal class Engine : IEngine
    {
        public async Task<HttpResponse> HandleRequest(HttpRequest request, CancellationToken cancellationToken)
        {
            // Simulate some request handling...
            await Task.Delay(TimeSpan.FromMilliseconds(300), cancellationToken).ConfigureAwait(false);

            return new HttpResponse();
        }
    }
}