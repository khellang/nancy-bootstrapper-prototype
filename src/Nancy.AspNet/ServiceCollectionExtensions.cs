using Microsoft.Extensions.DependencyInjection;
using Nancy.Bootstrappers.AspNet;
using Nancy.Core;

namespace Nancy.AspNet
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNancy(this IServiceCollection services)
        {
            return services.AddNancy(DefaultPlatformServices.Instance);
        }

        public static IServiceCollection AddNancy(this IServiceCollection services, IPlatformServices platformServices)
        {
            // For ASP.NET it only makes sense to use the AspNetBootstrapper since it handles container
            // customization etc. Therefore we hide away the whole bootstrapper concept.
            // We still need to provide a way to configure the application.

            // TODO: Could this be an Action delegate that we pass into the bootstrapper?

            var bootstrapper = new AspNetBootstrapper();

            return services.AddNancy(platformServices, bootstrapper);
        }

        public static IServiceCollection AddNancy(this IServiceCollection services,
            IPlatformServices platformServices,
            IBootstrapper<IServiceCollection, IServiceProvider> bootstrapper)
        {
            bootstrapper.Populate(services, platformServices);

            // Make sure we add the bootstrapper so it can be resolved in a call to `UseNancy`.
            return services.AddInstance<IBootstrapper<IServiceProvider>>(bootstrapper);
        }
    }
}