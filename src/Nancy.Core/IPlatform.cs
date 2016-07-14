namespace Nancy.Core
{
    using Microsoft.Extensions.PlatformAbstractions;
    using Nancy.Core.Scanning;

    public interface IPlatform
    {
        ApplicationEnvironment Environment { get; }

        IAssemblyCatalog AssemblyCatalog { get; }

        ITypeCatalog TypeCatalog { get; }

        IBootstrapperLocator BootstrapperLocator { get; }
    }
}
