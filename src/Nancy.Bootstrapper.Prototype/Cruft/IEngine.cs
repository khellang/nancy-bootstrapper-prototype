using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication7.Cruft.Http;

namespace ConsoleApplication7.Cruft
{
    public interface IEngine
    {
        Task<HttpResponse> HandleRequest(HttpRequest request, CancellationToken cancellationToken);
    }
}