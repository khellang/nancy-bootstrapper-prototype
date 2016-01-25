using Microsoft.Extensions.DependencyInjection;
using Nancy.Core;

namespace Nancy.Bootstrappers.AspNet
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