namespace Nancy.Owin
{
    using global::Owin;
    using Nancy.Core;

    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseNancy(this IAppBuilder app)
        {
            return app.UseNancy(DefaultPlatformServices.Instance);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IPlatformServices platformServices)
        {
            var bootstrapper = platformServices.BootstrapperLocator.GetBootstrapper();

            return app.UseNancy(platformServices, bootstrapper);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IBootstrapper bootstrapper)
        {
            return app.UseNancy(DefaultPlatformServices.Instance, bootstrapper);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IPlatformServices platformServices, IBootstrapper bootstrapper)
        {
            var application = bootstrapper.InitializeApplication(platformServices);

            return app.UseNancy(application);
        }

        public static IAppBuilder UseNancy<TContainer>(this IAppBuilder app, IBootstrapper<TContainer> bootstrapper, TContainer container)
        {
            var application = bootstrapper.InitializeApplication(container);

            return app.UseNancy(application);
        }

        public static IAppBuilder UseNancy(this IAppBuilder app, IApplication application)
        {
            return app.Use(NancyMiddleware.Create(application));
        }
    }
}
