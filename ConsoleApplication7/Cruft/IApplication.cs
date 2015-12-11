using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication7.Cruft.Http;

namespace ConsoleApplication7.Cruft
{
    public interface IApplication : IDisposable
    {
        Task<HttpResponse> HandleRequest(HttpRequest request, CancellationToken cancellationToken);
    }
}