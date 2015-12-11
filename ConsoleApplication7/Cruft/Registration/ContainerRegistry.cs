using System.Collections.Generic;

namespace ConsoleApplication7.Cruft.Registration
{
    public class ContainerRegistry : IContainerRegistry
    {
        public ContainerRegistry(IReadOnlyCollection<TypeRegistration> typeRegistrations,
            IReadOnlyCollection<CollectionTypeRegistration> collectionTypeRegistrations,
            IReadOnlyCollection<InstanceRegistration> instanceRegistrations)
        {
            TypeRegistrations = typeRegistrations;
            CollectionTypeRegistrations = collectionTypeRegistrations;
            InstanceRegistrations = instanceRegistrations;
        }

        public IReadOnlyCollection<TypeRegistration> TypeRegistrations { get; }

        public IReadOnlyCollection<CollectionTypeRegistration> CollectionTypeRegistrations { get; }

        public IReadOnlyCollection<InstanceRegistration> InstanceRegistrations { get; }
    }
}