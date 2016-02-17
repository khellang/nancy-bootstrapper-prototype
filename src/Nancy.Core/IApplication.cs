namespace Nancy.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core.Http;

    public interface IApplication : IDisposable
    {
        Task HandleRequest(HttpContext context, CancellationToken cancellationToken);
    }
}
