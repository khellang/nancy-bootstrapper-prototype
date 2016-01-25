using System;
using System.Collections.Generic;
using Nancy.Core.Registration;
using Nancy.Core.Scanning;

namespace Nancy.Core.Configuration
{
    public class FrameworkConfiguration : IFrameworkConfiguration
    {
        private static readonly Type[] DefaultSerializerTypes = { typeof(JsonNetSerializer) };

        public FrameworkConfiguration()
        {
            TypeRegistrationFactories = new List<IRegistrationFactory<TypeRegistration>>
            {
                (Engine = new TypeRegistrationFactory<IEngine, Engine>(Lifetime.Scoped))
            };

            CollectionTypeRegistrationFactories = new List<IRegistrationFactory<CollectionTypeRegistration>>
            {
                (Serializers = new CollectionTypeRegistrationFactory<ISerializer>(Lifetime.Singleton, DefaultSerializerTypes))
            };
        }

        public ITypeRegistrationFactory<IEngine> Engine { get; }

        public ICollectionTypeRegistrationFactory<ISerializer> Serializers { get; }

        private List<IRegistrationFactory<TypeRegistration>> TypeRegistrationFactories { get; }

        private List<IRegistrationFactory<CollectionTypeRegistration>> CollectionTypeRegistrationFactories { get; }

        public IContainerRegistry GetRegistry(ITypeCatalog typeCatalog)
        {
            var typeRegistrations = typeCatalog.GetRegistrations(TypeRegistrationFactories);

            var collectionTypeRegistrations = typeCatalog.GetRegistrations(CollectionTypeRegistrationFactories);

            return new ContainerRegistry(typeRegistrations, collectionTypeRegistrations);
        }
    }
}