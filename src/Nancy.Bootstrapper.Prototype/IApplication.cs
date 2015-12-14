using System;
using System.Threading;
using System.Threading.Tasks;
using Nancy.Bootstrapper.Prototype.Http;

namespace Nancy.Bootstrapper.Prototype
{
    public interface IApplication : IDisposable
    {
        Task<HttpResponse> HandleRequest(HttpRequest request, CancellationToken cancellationToken);
    }
}