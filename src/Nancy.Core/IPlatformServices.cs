namespace Nancy.Core
{
    using Nancy.Core.Scanning;

    public interface IPlatformServices
    {
        IAssemblyCatalog AssemblyCatalog { get; }

        ITypeCatalog TypeCatalog { get; }

        IBootstrapperLocator BootstrapperLocator { get; }
    }
}
