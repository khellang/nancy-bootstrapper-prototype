using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Nancy.Bootstrapper.Prototype
{
    public interface IApplication : IDisposable
    {
        Task HandleRequest(HttpContext context, CancellationToken cancellationToken);
    }
}