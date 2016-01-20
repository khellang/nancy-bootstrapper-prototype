using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class CustomEngine : IEngine, IDisposable
    {
        public CustomEngine(IRequestService requestService, IEnumerable<ISerializer> serializers)
        {
            System.Console.WriteLine("Created CustomEngine.");

            RequestService = requestService;
            Serializers = serializers;
        }

        private IRequestService RequestService { get; }

        private IEnumerable<ISerializer> Serializers { get; }

        public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            if (RequestService == null)
            {
                throw new InvalidOperationException("RequestService should've been injected!");
            }

            return Task.FromResult(0);
        }

        public void Dispose()
        {
            System.Console.WriteLine("Disposed CustomEngine.");
        }
    }
}