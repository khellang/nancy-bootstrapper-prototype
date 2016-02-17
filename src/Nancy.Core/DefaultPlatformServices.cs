namespace Nancy.Core
{
    using System;
    using Microsoft.Extensions.PlatformAbstractions;
    using Nancy.Core.Scanning;

    public class DefaultPlatformServices : IPlatformServices
    {
        private static readonly Lazy<IPlatformServices> DefaultInstance =
            new Lazy<IPlatformServices>(() => new DefaultPlatformServices());

        public DefaultPlatformServices()
        {
            // TODO: If LibraryManager is null here, we should use DependencyContext.
            var libraryManager = PlatformServices.Default.LibraryManager;

            // TODO: We need a fallback AssemblyCatalog for full framework. AppDomainAssemblyCatalog?

            this.AssemblyCatalog = new LibraryManagerAssemblyCatalog(libraryManager);
            this.TypeCatalog = new TypeCatalog(this.AssemblyCatalog);
            this.BootstrapperLocator = new BootstrapperLocator(this.TypeCatalog);
        }

        public static IPlatformServices Instance => DefaultInstance.Value;

        public IAssemblyCatalog AssemblyCatalog { get; }

        public ITypeCatalog TypeCatalog { get; }

        public IBootstrapperLocator BootstrapperLocator { get; }
    }
}
