namespace Nancy.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Nancy.Bootstrappers.AspNetCore;
    using Nancy.Core;
    using Nancy.Core.Configuration;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNancy(this IServiceCollection services)
        {
            return services.AddNancy(PlatformServices.Default.Application);
        }

        public static IServiceCollection AddNancy(this IServiceCollection services,
            ApplicationEnvironment environment)
        {
            return services.AddNancy(environment, configure => { /* ignore */ });
        }

        public static IServiceCollection AddNancy(this IServiceCollection services,
            Action<IApplicationConfiguration> configure)
        {
            return AddNancy(services, PlatformServices.Default.Application, configure);
        }

        public static IServiceCollection AddNancy(this IServiceCollection services,
            ApplicationEnvironment environment,
            Action<IApplicationConfiguration> configure)
        {
            Check.NotNull(environment, nameof(environment));

            var platformServices = new DefaultPlatformServices(environment);

            return services.AddNancy(platformServices, configure);
        }

        public static IServiceCollection AddNancy(this IServiceCollection services,
            IPlatformServices platformServices,
            Action<IApplicationConfiguration> configure)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(platformServices, nameof(platformServices));
            Check.NotNull(configure, nameof(configure));

            // For ASP.NET it only makes sense to use the AspNetBootstrapper since it handles container
            // customization etc. Therefore we hide away the whole bootstrapper concept.
            // We still need to provide a way to configure the application. This
            // is done by passing a delegate to a special inline bootstrapper.
            var bootstrapper = new InlineAspNetBootstrapper(configure).Populate(services, platformServices);

            // Make sure we add the bootstrapper so it can be resolved in a call to `UseNancy`.
            return services.AddSingleton(bootstrapper);
        }

        private class InlineAspNetBootstrapper : AspNetBootstrapper
        {
            private readonly Action<IApplicationConfiguration<IServiceCollection>> configure;

            public InlineAspNetBootstrapper(Action<IApplicationConfiguration> configure)
            {
                Check.NotNull(configure, nameof(configure));

                this.configure = configure;
            }

            protected override void ConfigureApplication(IApplicationConfiguration<IServiceCollection> app)
            {
                this.configure.Invoke(app);
            }
        }
    }
}
