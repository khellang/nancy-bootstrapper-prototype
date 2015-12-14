using System.Collections.Generic;
using Nancy.Bootstrapper.Prototype.Cruft.Registration;

namespace Nancy.Bootstrapper.Prototype.Cruft
{
    public class ApplicationBuilder<TContainer> : IApplicationBuilder<TContainer>
    {
        public ApplicationBuilder(TContainer container, ITypeCatalog typeCatalog)
        {
            Container = container;
            TypeCatalog = typeCatalog;
        }

        public TContainer Container { get; }

        public ITypeCatalog TypeCatalog { get; }

        public IContainerRegistry BuildRegistry()
        {
            var typeRegistrations = new List<TypeRegistration>
            {
                TypeRegistration.Create<IEngine, Engine>(Lifetime.Scoped),
            };

            var collectionTypeRegistrations = new List<CollectionTypeRegistration>();

            var instanceRegistrations = new List<InstanceRegistration>();

            return new ContainerRegistry(typeRegistrations, collectionTypeRegistrations, instanceRegistrations);
        }
    }
}