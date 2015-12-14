using System.Collections.Generic;
using System.Linq;
using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype
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
                CreateRegistration<IEngine, Engine>(Lifetime.Scoped)
            };

            var collectionTypeRegistrations = new List<CollectionTypeRegistration>();

            var instanceRegistrations = new List<InstanceRegistration>();

            return new ContainerRegistry(typeRegistrations, collectionTypeRegistrations, instanceRegistrations);
        }

        private TypeRegistration CreateRegistration<TService, TDefaultImplementation>(Lifetime lifetime)
        {
            // TODO: Should we throw if multiple types are found?
            var customImplementationType = TypeCatalog
                .TypesOf<TService>(ScanMode.ExcludeNancy)
                .FirstOrDefault();

            var implementationType = customImplementationType ?? typeof(TDefaultImplementation);

            return TypeRegistration.Create<TService>(implementationType, lifetime);
        }
    }
}