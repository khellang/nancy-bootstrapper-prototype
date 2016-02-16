using Microsoft.Extensions.DependencyInjection;
using Nancy.Bootstrappers.AspNet;
using Nancy.Core;

namespace Nancy.AspNet
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNancy(this IServiceCollection services)
        {
            var bootstrapper = new AspNetBootstrapper();

            bootstrapper.Populate(services);

            return services.AddInstance<IBootstrapper<IServiceProvider>>(bootstrapper);
        }
    }
}