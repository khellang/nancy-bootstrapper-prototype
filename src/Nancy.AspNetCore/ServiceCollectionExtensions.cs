namespace Nancy.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.Bootstrappers.AspNetCore;
    using Nancy.Core;
    using Nancy.Core.Configuration;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNancy(this IServiceCollection services)
        {
            return services.AddNancy(configure => { /* ignore */ });
        }

        public static IServiceCollection AddNancy(this IServiceCollection services,
            Action<IApplicationConfiguration> configure)
        {
            return services.AddNancy(DefaultPlatform.Instance, configure);
        }

        public static IServiceCollection AddNancy(this IServiceCollection services,
            IPlatform platform,
            Action<IApplicationConfiguration> configure)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(platform, nameof(platform));
            Check.NotNull(configure, nameof(configure));

            // For ASP.NET it only makes sense to use the AspNetBootstrapper since it handles container
            // customization etc. Therefore we hide away the whole bootstrapper concept.
            // We still need to provide a way to configure the application. This
            // is done by passing a delegate to a special inline bootstrapper.
            var bootstrapper = new InlineAspNetBootstrapper(configure).Populate(services, platform);

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
