using Microsoft.AspNet.Builder;

namespace Nancy.Core
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNancy<TContainer>(this IApplicationBuilder builder,
            IBootstrapper<TContainer> bootstrapper,
            TContainer container)
        {
            var application = bootstrapper.InitializeApplication(container);

            return builder.Use((context, next) => application.HandleRequest(context, context.RequestAborted));
        }
    }
}