namespace Nancy.Owin
{
    using global::Owin;
    using Microsoft.Extensions.PlatformAbstractions;
    using Nancy.Core;

    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseNancy(this IAppBuilder app)
        {
            return app.UseNancy(PlatformServices.Default.Application);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, ApplicationEnvironment environment)
        {
            Check.NotNull(environment, nameof(environment));

            var platformServices = new DefaultPlatformServices(environment);

            return app.UseNancy(platformServices);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IPlatformServices platformServices)
        {
            Check.NotNull(platformServices, nameof(platformServices));

            var bootstrapper = platformServices.BootstrapperLocator.GetBootstrapper();

            return app.UseNancy(bootstrapper, platformServices);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IBootstrapper bootstrapper)
        {
            return app.UseNancy(bootstrapper, PlatformServices.Default.Application);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IBootstrapper bootstrapper, ApplicationEnvironment environment)
        {
            Check.NotNull(environment, nameof(environment));

            var platformServices = new DefaultPlatformServices(environment);

            return app.UseNancy(bootstrapper, platformServices);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IBootstrapper bootstrapper, IPlatformServices platformServices)
        {
            Check.NotNull(platformServices, nameof(platformServices));
            Check.NotNull(bootstrapper, nameof(bootstrapper));

            var application = bootstrapper.InitializeApplication(platformServices);

            return app.UseNancy(application);
        }

        public static IAppBuilder UseNancy<TContainer>(this IAppBuilder app, IBootstrapper<TContainer> bootstrapper, TContainer container)
        {
            Check.NotNull(bootstrapper, nameof(bootstrapper));
            Check.NotNull(container, nameof(container));

            var application = bootstrapper.InitializeApplication(container);

            return app.UseNancy(application);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IApplication application)
        {
            Check.NotNull(app, nameof(app));
            Check.NotNull(application, nameof(application));

            return app.Use(NancyMiddleware.Create(application));
        }
    }
}
