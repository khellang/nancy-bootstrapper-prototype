using System;
using System.Threading;
using System.Threading.Tasks;
using Nancy.Core.Http;

namespace Nancy.Core
{
    public interface IApplication : IDisposable
    {
        Task HandleRequest(HttpContext context, CancellationToken cancellationToken);
    }
}