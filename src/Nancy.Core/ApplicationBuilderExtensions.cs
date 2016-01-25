using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace Nancy.Core
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNancy<TContainer>(this IApplicationBuilder builder,
            IBootstrapper<TContainer> bootstrapper,
            TContainer container)
        {
            return builder.UseNancy(bootstrapper.InitializeApplication(container));
        }

        public static IApplicationBuilder UseNancy(this IApplicationBuilder builder)
        {
            return builder.UseNancy(PlatformServices.Default.BootstrapperLocator.GetBootstrapper());
        }

        public static IApplicationBuilder UseNancy(this IApplicationBuilder builder, IBootstrapper bootstrapper)
        {
            return builder.UseNancy(bootstrapper.InitializeApplication());
        }

        public static IApplicationBuilder UseNancy(this IApplicationBuilder builder, IApplication application)
        {
            return builder.UseMiddleware<NancyMiddleware>(application);
        }

        private class NancyMiddleware : IDisposable
        {
            public NancyMiddleware(RequestDelegate next, IApplication application)
            {
                Next = next;
                Application = application;
            }

            public Task Invoke(HttpContext context)
            {
                return Application.HandleRequest(context, context.RequestAborted);
            }

            private RequestDelegate Next { get; }

            private IApplication Application { get; }

            public void Dispose()
            {
                Application.Dispose();
            }
        }
    }
}