using System;
using Microsoft.Extensions.PlatformAbstractions;
using Nancy.Core.Scanning;

namespace Nancy.Core
{
    public class DefaultPlatformServices : IPlatformServices
    {
        private static readonly Lazy<IPlatformServices> DefaultInstance =
            new Lazy<IPlatformServices>(() => new DefaultPlatformServices());

        public DefaultPlatformServices()
        {
            // TODO: If LibraryManager is null here, we should use DependencyContext.
            var libraryManager = PlatformServices.Default.LibraryManager;

            // TODO: We need a fallback AssemblyCatalog for full framework. AppDomainAssemblyCatalog?

            AssemblyCatalog = new LibraryManagerAssemblyCatalog(libraryManager);
            TypeCatalog = new TypeCatalog(AssemblyCatalog);
            BootstrapperLocator = new BootstrapperLocator(TypeCatalog);
        }

        public static IPlatformServices Instance => DefaultInstance.Value;

        public IAssemblyCatalog AssemblyCatalog { get; }

        public ITypeCatalog TypeCatalog { get; }

        public IBootstrapperLocator BootstrapperLocator { get; }
    }
}
