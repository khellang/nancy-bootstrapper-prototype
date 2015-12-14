using System.Threading;
using System.Threading.Tasks;
using Nancy.Bootstrapper.Prototype.Http;

namespace Nancy.Bootstrapper.Prototype
{
    public interface IEngine
    {
        Task<HttpResponse> HandleRequest(HttpRequest request, CancellationToken cancellationToken);
    }
}