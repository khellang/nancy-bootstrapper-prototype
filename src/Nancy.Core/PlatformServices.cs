using Nancy.Core.Registration;
using Nancy.Core.Scanning;

namespace Nancy.Core
{
    public abstract class PlatformServices
    {
        public static PlatformServices Default { get; private set; } = new DefaultPlatformServices();

        public abstract IAssemblyCatalog AssemblyCatalog { get; }

        public abstract ITypeCatalog TypeCatalog { get; }

        public abstract IBootstrapperLocator BootstrapperLocator { get; }

        public IContainerRegistry GetRegistry()
        {
            var typeCatalog = TypeCatalog.AsInstanceRegistration();
            var assemblyCatalog = AssemblyCatalog.AsInstanceRegistration();
            var bootstrapperLocator = BootstrapperLocator.AsInstanceRegistration();

            var registrations = new[] { typeCatalog, assemblyCatalog, bootstrapperLocator };

            return new ContainerRegistry(instanceRegistrations: registrations);
        }

        public static void SetDefault(PlatformServices @default)
        {
            Default = @default;
        }
    }
}