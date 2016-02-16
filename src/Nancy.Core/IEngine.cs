using System.Threading;
using System.Threading.Tasks;
using Nancy.Core.Http;

namespace Nancy.Core
{
    public interface IEngine
    {
        Task HandleRequest(HttpContext context, CancellationToken cancellationToken);
    }
}