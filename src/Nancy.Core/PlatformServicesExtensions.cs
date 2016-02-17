namespace Nancy.Core
{
    using Nancy.Core.Registration;

    internal static class PlatformServicesExtensions
    {
        public static IContainerRegistry GetRegistry(this IPlatformServices platformServices)
        {
            Check.NotNull(platformServices, nameof(platformServices));

            var bootstrapperLocator = platformServices.BootstrapperLocator.AsInstanceRegistration();
            var assemblyCatalog = platformServices.AssemblyCatalog.AsInstanceRegistration();
            var typeCatalog = platformServices.TypeCatalog.AsInstanceRegistration();

            var registrations = new[] { bootstrapperLocator, assemblyCatalog, typeCatalog };

            return new ContainerRegistry(instanceRegistrations: registrations);
        }

        private static InstanceRegistration AsInstanceRegistration<TService>(this TService instance)
        {
            Check.NotNull(instance, nameof(instance));

            return new InstanceRegistration(typeof(TService), instance);
        }
    }
}
