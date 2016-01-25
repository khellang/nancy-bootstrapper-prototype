using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Core;

namespace Nancy.Bootstrappers.AspNet
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNancy(this IApplicationBuilder builder)
        {
            var provider = new DisposableServiceProvider(builder.ApplicationServices);

            var bootstrapper = builder.ApplicationServices.GetRequiredService<IBootstrapper<IServiceProvider>>();

            var application = bootstrapper.InitializeApplication(provider);

            return builder.UseMiddleware<NancyMiddleware>(application);
        }

        private class NancyMiddleware : IDisposable
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
                return Application.HandleRequest(context, context.RequestAborted);
            }

            public void Dispose()
            {
                Application.Dispose();
            }
        }
    }
}