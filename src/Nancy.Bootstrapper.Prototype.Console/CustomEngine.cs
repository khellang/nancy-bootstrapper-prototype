using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class CustomEngine : IEngine
    {
        public CustomEngine(IRequestService requestService)
        {
            RequestService = requestService;
        }

        private IRequestService RequestService { get; }

        public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            if (RequestService == null)
            {
                throw new InvalidOperationException("RequestService should've been injected!");
            }

            return Task.FromResult(0);
        }
    }
}