using Nancy.Core.Scanning;
using MsPlatformServices = Microsoft.Extensions.PlatformAbstractions.PlatformServices;

namespace Nancy.Core
{
    internal sealed class DefaultPlatformServices : PlatformServices
    {
        public DefaultPlatformServices()
        {
            AssemblyCatalog = new LibraryManagerAssemblyCatalog(MsPlatformServices.Default.LibraryManager);
            TypeCatalog = new TypeCatalog(AssemblyCatalog);
            BootstrapperLocator = new BootstrapperLocator(TypeCatalog);
        }

        public override IAssemblyCatalog AssemblyCatalog { get; }

        public override ITypeCatalog TypeCatalog { get; }

        public override IBootstrapperLocator BootstrapperLocator { get; }
    }
}