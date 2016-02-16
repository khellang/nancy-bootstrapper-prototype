namespace Nancy.Core
{
    public static class BootstrapperExtensions
    {
        public static void Populate<TBuilder, TContainer>(
            this IBootstrapper<TBuilder, TContainer> bootstrapper,
            TBuilder builder)
        {
            bootstrapper.Populate(builder, DefaultPlatformServices.Instance);
        }
    }
}