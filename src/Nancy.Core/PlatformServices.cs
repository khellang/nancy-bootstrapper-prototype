using Nancy.Core.Scanning;

namespace Nancy.Core
{
    public interface IPlatformServices
    {
        IAssemblyCatalog AssemblyCatalog { get; }

        ITypeCatalog TypeCatalog { get; }

        IBootstrapperLocator BootstrapperLocator { get; }
    }
}