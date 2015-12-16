using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Nancy.Bootstrapper.Prototype
{
    public class Engine : IEngine
    {
        public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            // Simulate some request handling...
            return Task.Delay(TimeSpan.FromMilliseconds(300), cancellationToken);
        }
    }
}