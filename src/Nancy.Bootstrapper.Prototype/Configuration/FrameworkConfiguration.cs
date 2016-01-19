using System;
using System.Collections.Generic;
using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public class FrameworkConfiguration : IFrameworkConfiguration
    {
        private static readonly Type[] DefaultSerializerTypes = { typeof(JsonNetSerializer) };

        public FrameworkConfiguration()
        {
            TypeRegistrationFactories = new IRegistrationFactory<TypeRegistration>[]
            {
                Engine = new TypeRegistrationFactory<IEngine, Engine>(Lifetime.Scoped)
            };

            CollectionTypeRegistrationFactories = new IRegistrationFactory<CollectionTypeRegistration>[]
            {
                Serializers = new CollectionTypeRegistrationFactory<ISerializer>(Lifetime.Singleton, DefaultSerializerTypes)
            };
        }

        public ITypeRegistrationFactory<IEngine> Engine { get; }

        public ICollectionTypeRegistrationFactory<ISerializer> Serializers { get; }

        private IList<IRegistrationFactory<TypeRegistration>> TypeRegistrationFactories { get; }

        private IList<IRegistrationFactory<CollectionTypeRegistration>> CollectionTypeRegistrationFactories { get; }

        public IContainerRegistry GetRegistry(ITypeCatalog typeCatalog)
        {
            var typeRegistrations = typeCatalog.GetRegistrations(TypeRegistrationFactories);

            var collectionTypeRegistrations = typeCatalog.GetRegistrations(CollectionTypeRegistrationFactories);

            // TODO: What to do about instance registrations?
            var instanceRegistrations = new List<InstanceRegistration>();

            return new ContainerRegistry(typeRegistrations, collectionTypeRegistrations, instanceRegistrations);
        }
    }
}