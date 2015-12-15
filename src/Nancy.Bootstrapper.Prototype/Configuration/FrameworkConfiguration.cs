using System.Collections.Generic;
using System.Linq;
using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public class FrameworkConfiguration : IFrameworkConfiguration
    {
        public FrameworkConfiguration()
        {
            Factories = new List<IRegistrationFactory>();

            Factories.Add(Engine = new RegistrationFactory<IEngine, Engine>(Lifetime.Scoped));
        }

        public IRegistrationFactory<IEngine> Engine { get; }

        private IList<IRegistrationFactory> Factories { get; }

        public IContainerRegistry GetRegistry(ITypeCatalog typeCatalog)
        {
            var typeRegistrations = Factories.Select(factory => factory.GetRegistration(typeCatalog)).ToList();

            var collectionTypeRegistrations = new List<CollectionTypeRegistration>();

            var instanceRegistrations = new List<InstanceRegistration>();

            return new ContainerRegistry(typeRegistrations, collectionTypeRegistrations, instanceRegistrations);
        }
    }
}