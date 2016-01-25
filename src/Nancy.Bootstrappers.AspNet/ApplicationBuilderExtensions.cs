using Microsoft.AspNet.Builder;
using Nancy.Core;

namespace Nancy.Bootstrappers.AspNet
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNancy(this IApplicationBuilder builder, IBootstrapper<IServiceProvider> bootstrapper)
        {
            var provider = new DisposableServiceProvider(builder.ApplicationServices);

            return builder.UseNancy(bootstrapper.InitializeApplication(provider));
        }
    }
}