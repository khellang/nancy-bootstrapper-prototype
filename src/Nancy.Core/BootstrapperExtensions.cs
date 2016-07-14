namespace Nancy.Core
{
    using Microsoft.Extensions.PlatformAbstractions;

    public static class BootstrapperExtensions
    {
        public static IBootstrapper<TContainer> Populate<TBuilder, TContainer>(this IBootstrapper<TBuilder, TContainer> bootstrapper, TBuilder builder)
        {
            return bootstrapper.Populate(builder, PlatformServices.Default.Application);
        }

        public static IBootstrapper<TContainer> Populate<TBuilder, TContainer>(this IBootstrapper<TBuilder, TContainer> bootstrapper, TBuilder builder, ApplicationEnvironment environment)
        {
            return bootstrapper.Populate(builder, new DefaultPlatform(environment));
        }
    }
}
