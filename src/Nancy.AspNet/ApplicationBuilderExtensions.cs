using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Core;
using Nancy.Bootstrappers.AspNet;

namespace Nancy.AspNet
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNancy(this IApplicationBuilder builder)
        {
            // We need to wrap the default ApplicationServices
            // because IServiceProvider is not implementing IDisposable.
            var provider = builder.ApplicationServices.AsDisposable(shouldDispose: false);

            // We assume that the user has already called `AddNancy`
            // in `ConfigureServices`, otherwise this will fail.
            var bootstrapper = provider.GetRequiredService<IBootstrapper<IDisposableServiceProvider>>();

            var application = bootstrapper.InitializeApplication(provider);

            return builder.UseMiddleware<NancyMiddleware>(application);
        }

        private class NancyMiddleware
        {
            public NancyMiddleware(RequestDelegate next, IApplication application)
            {
                Next = next;
                Application = application;
            }

            private RequestDelegate Next { get; }

            private IApplication Application { get; }

            public Task Invoke(HttpContext context)
            {
                var httpContext = new AspNetHttpContext(context);

                // Add the request services so we can get it out in the bootstrapper.
                httpContext.Items.Add(Constants.AspNetRequestServices, context.RequestServices);

                var cancellationToken = context.RequestAborted;

                return Application.HandleRequest(httpContext, cancellationToken);
            }
        }
    }
}