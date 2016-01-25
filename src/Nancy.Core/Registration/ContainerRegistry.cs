using System;
using System.Collections.Generic;

namespace Nancy.Core.Registration
{
    public class ContainerRegistry : IContainerRegistry
    {
        public ContainerRegistry(IReadOnlyCollection<TypeRegistration> typeRegistrations = null,
            IReadOnlyCollection<CollectionTypeRegistration> collectionTypeRegistrations = null,
            IReadOnlyCollection<InstanceRegistration> instanceRegistrations = null)
        {
            TypeRegistrations = typeRegistrations ?? Array.Empty<TypeRegistration>();
            CollectionTypeRegistrations = collectionTypeRegistrations ?? Array.Empty<CollectionTypeRegistration>();
            InstanceRegistrations = instanceRegistrations ?? Array.Empty<InstanceRegistration>();
        }

        public IReadOnlyCollection<TypeRegistration> TypeRegistrations { get; }

        public IReadOnlyCollection<CollectionTypeRegistration> CollectionTypeRegistrations { get; }

        public IReadOnlyCollection<InstanceRegistration> InstanceRegistrations { get; }
    }
}