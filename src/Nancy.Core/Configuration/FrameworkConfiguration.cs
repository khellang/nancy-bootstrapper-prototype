namespace Nancy.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using Nancy.Core.Registration;
    using Nancy.Core.Scanning;

    public class FrameworkConfiguration : IFrameworkConfiguration
    {
        private static readonly Type[] DefaultSerializerTypes = { typeof(JsonNetSerializer) };

        private readonly List<IRegistrationFactory<CollectionTypeRegistration>> collectionTypeRegistrationFactories;

        private readonly List<IRegistrationFactory<TypeRegistration>> typeRegistrationFactories;

        public FrameworkConfiguration()
        {
            this.typeRegistrationFactories = new List<IRegistrationFactory<TypeRegistration>>
            {
                (this.Engine = new TypeRegistrationFactory<IEngine, Engine>(Lifetime.PerRequest))
            };

            this.collectionTypeRegistrationFactories = new List<IRegistrationFactory<CollectionTypeRegistration>>
            {
                (this.Serializers = new CollectionTypeRegistrationFactory<ISerializer>(Lifetime.Singleton, DefaultSerializerTypes))
            };
        }

        public ITypeRegistrationFactory<IEngine> Engine { get; }

        public ICollectionTypeRegistrationFactory<ISerializer> Serializers { get; }

        public IContainerRegistry GetRegistry(ITypeCatalog typeCatalog)
        {
            var typeRegistrations = typeCatalog.GetRegistrations(this.typeRegistrationFactories);

            var collectionTypeRegistrations = typeCatalog.GetRegistrations(this.collectionTypeRegistrationFactories);

            return new ContainerRegistry(typeRegistrations, collectionTypeRegistrations);
        }
    }
}
