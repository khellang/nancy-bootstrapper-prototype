using Nancy.Bootstrapper.Prototype.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class CustomEngine : IEngine
    {
        public CustomEngine(IRequestService requestService)
        {
            RequestService = requestService;
        }

        private IRequestService RequestService { get; }

        public Task<HttpResponse> HandleRequest(HttpRequest request, CancellationToken cancellationToken)
        {
            if (RequestService == null)
            {
                throw new InvalidOperationException("RequestService should've been injected!");
            }

            return Task.FromResult(new HttpResponse());
        }
    }
}