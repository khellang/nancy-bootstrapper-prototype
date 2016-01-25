using System.Collections.Generic;

namespace Nancy.Core.Registration
{
    public class ContainerRegistry : IContainerRegistry
    {
        private static readonly TypeRegistration[] EmptyTypeRegistrations = new TypeRegistration[0];

        private static readonly CollectionTypeRegistration[] EmptyCollectionTypeRegistrations = new CollectionTypeRegistration[0];

        private static readonly InstanceRegistration[] EmptyInstanceRegistrations = new InstanceRegistration[0];

        public ContainerRegistry(IReadOnlyCollection<TypeRegistration> typeRegistrations = null,
            IReadOnlyCollection<CollectionTypeRegistration> collectionTypeRegistrations = null,
            IReadOnlyCollection<InstanceRegistration> instanceRegistrations = null)
        {
            TypeRegistrations = typeRegistrations ?? EmptyTypeRegistrations;
            CollectionTypeRegistrations = collectionTypeRegistrations ?? EmptyCollectionTypeRegistrations;
            InstanceRegistrations = instanceRegistrations ?? EmptyInstanceRegistrations;
        }

        public IReadOnlyCollection<TypeRegistration> TypeRegistrations { get; }

        public IReadOnlyCollection<CollectionTypeRegistration> CollectionTypeRegistrations { get; }

        public IReadOnlyCollection<InstanceRegistration> InstanceRegistrations { get; }
    }
}