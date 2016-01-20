using Nancy.Core.Scanning;

namespace Nancy.Core
{
    public abstract class PlatformServices
    {
        public static PlatformServices Default { get; private set; } = new DefaultPlatformServices();

        public abstract IAssemblyCatalog AssemblyCatalog { get; }

        public abstract ITypeCatalog TypeCatalog { get; }

        public abstract IBootstrapperLocator BootstrapperLocator { get; }

        public static void SetDefault(PlatformServices @default)
        {
            Default = @default;
        }
    }
}