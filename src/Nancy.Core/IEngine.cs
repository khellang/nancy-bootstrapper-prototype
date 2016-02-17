namespace Nancy.Core
{
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core.Http;

    public interface IEngine
    {
        Task HandleRequest(HttpContext context, CancellationToken cancellationToken);
    }
}
