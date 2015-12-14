using System.Collections.Generic;
using Nancy.Bootstrapper.Prototype.Cruft.Registration;

namespace Nancy.Bootstrapper.Prototype.Cruft
{
    internal class ApplicationBuilder<TContainer> : IApplicationBuilder<TContainer>
    {
        public ApplicationBuilder(TContainer container)
        {
            Container = container;
        }

        public TContainer Container { get; }

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