namespace Nancy.Core
{
    using System;
    using System.Linq;
    using Nancy.Core.Scanning;

    public class BootstrapperLocator : IBootstrapperLocator
    {
        private readonly ITypeCatalog typeCatalog;

        public BootstrapperLocator(ITypeCatalog typeCatalog)
        {
            Check.NotNull(typeCatalog, nameof(typeCatalog));

            this.typeCatalog = typeCatalog;
        }

        public IBootstrapper GetBootstrapper()
        {
            // TODO: Should we throw if multiple types are found?
            var bootstrapperTypes = this.typeCatalog
                .GetTypesAssignableTo<IBootstrapper>(ScanningStrategies.ExcludeNancy)
                .ToArray();

            var bootstrapperType = bootstrapperTypes.FirstOrDefault();

            if (bootstrapperType == null)
            {
                // TODO: Return default bootstrapper.
                throw new InvalidOperationException("Could not locate a bootstrapper implementation.");
            }

            // TODO: Wrap potential exception with a better error message.
            return (IBootstrapper)Activator.CreateInstance(bootstrapperType);
        }
    }
}
