namespace Nancy.Owin
{
    using global::Owin;
    using Nancy.Core;

    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseNancy(this IAppBuilder app)
        {
            return app.UseNancy(DefaultPlatform.Instance);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IPlatform platform)
        {
            Check.NotNull(platform, nameof(platform));

            var bootstrapper = platform.BootstrapperLocator.GetBootstrapper();

            return app.UseNancy(bootstrapper, platform);
        }

        public static IAppBuilder UseNancy<TBootstrapper>(this IAppBuilder app) where TBootstrapper : IBootstrapper, new()
        {
            return app.UseNancy(new TBootstrapper());
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IBootstrapper bootstrapper)
        {
            return app.UseNancy(bootstrapper, DefaultPlatform.Instance);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IBootstrapper bootstrapper, IPlatform platform)
        {
            Check.NotNull(platform, nameof(platform));
            Check.NotNull(bootstrapper, nameof(bootstrapper));

            var application = bootstrapper.InitializeApplication(platform);

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
