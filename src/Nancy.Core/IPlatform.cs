namespace Nancy.Core
{
    using Nancy.Core.Scanning;

    public interface IPlatform
    {
        IAssemblyCatalog AssemblyCatalog { get; }

        ITypeCatalog TypeCatalog { get; }

        IBootstrapperLocator BootstrapperLocator { get; }
    }
}
