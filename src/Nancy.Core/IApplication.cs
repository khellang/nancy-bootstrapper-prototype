using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Nancy.Core
{
    public interface IApplication : IDisposable
    {
        Task HandleRequest(HttpContext context, CancellationToken cancellationToken);
    }
}