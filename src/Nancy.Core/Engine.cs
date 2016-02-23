namespace Nancy.Core
{
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core.Http;

    public class Engine : IEngine
    {
        public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            Check.NotNull(context, nameof(context));

            return Task.FromResult(0);
        }
    }
}
