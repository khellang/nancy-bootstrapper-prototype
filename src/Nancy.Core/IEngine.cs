using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace Nancy.Core
{
    public interface IEngine
    {
        Task HandleRequest(HttpContext context, CancellationToken cancellationToken);
    }
}