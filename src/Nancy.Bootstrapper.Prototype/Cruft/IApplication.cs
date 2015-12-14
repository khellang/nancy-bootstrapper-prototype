using System;
using System.Threading;
using System.Threading.Tasks;
using Nancy.Bootstrapper.Prototype.Cruft.Http;

namespace Nancy.Bootstrapper.Prototype.Cruft
{
    public interface IApplication : IDisposable
    {
        Task<HttpResponse> HandleRequest(HttpRequest request, CancellationToken cancellationToken);
    }
}