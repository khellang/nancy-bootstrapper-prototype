namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core;
    using Nancy.Core.Http;

    public class CustomEngine : IEngine, IDisposable
    {
        public CustomEngine(IRequestService requestService, IEnumerable<ISerializer> serializers)
        {
            Console.WriteLine("Created CustomEngine.");

            this.RequestService = requestService;
            this.Serializers = serializers;
        }

        private IRequestService RequestService { get; }

        private IEnumerable<ISerializer> Serializers { get; }

        public void Dispose()
        {
            Console.WriteLine("Disposed CustomEngine.");
        }

        public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            if (this.RequestService == null)
            {
                throw new InvalidOperationException("RequestService should've been injected!");
            }

            return Tasks.CompletedTask;
        }
    }
}
