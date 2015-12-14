using System;
using System.Linq;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype
{
    public class BootstrapperLocator : IBootstrapperLocator
    {
        public BootstrapperLocator(ITypeCatalog typeCatalog)
        {
            TypeCatalog = typeCatalog;
        }

        private ITypeCatalog TypeCatalog { get; }

        public IBootstrapper GetBootstrapper()
        {
            // TODO: Should we throw if multiple types are found?
            var bootstrapper = TypeCatalog
                .TypesOf<IBootstrapper>(ScanMode.ExcludeNancy)
                .FirstOrDefault();

            if (bootstrapper == null)
            {
                // TODO: Return default bootstrapper.
                throw new InvalidOperationException("Could not locate a bootstrapper implementation.");
            }

            // TODO: Wrap potential exception with a better error message.
            return (IBootstrapper) Activator.CreateInstance(bootstrapper);
        }
    }
}