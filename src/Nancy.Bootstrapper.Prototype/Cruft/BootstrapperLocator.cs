using System;
using System.Linq;

namespace Nancy.Bootstrapper.Prototype.Cruft
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
            var bootstrappers = TypeCatalog.TypesOf<IBootstrapper>(ScanMode.ExcludeNancy);

            var bootstrapperType = bootstrappers.FirstOrDefault();

            if (bootstrapperType == null)
            {
                // TODO: Return default bootstrapper.
                throw new InvalidOperationException("Could not locate a bootstrapper implementation.");
            }

            // TODO: Wrap potential exception with a better error message.
            return (IBootstrapper) Activator.CreateInstance(bootstrapperType);
        }
    }
}