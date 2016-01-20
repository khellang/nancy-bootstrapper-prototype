using System.Collections.Generic;

namespace Nancy.Bootstrapper.Prototype.Registration
{
    public interface IContainerRegistry
    {
        IReadOnlyCollection<TypeRegistration> TypeRegistrations { get; }

        IReadOnlyCollection<InstanceRegistration> InstanceRegistrations { get; }

        IReadOnlyCollection<CollectionTypeRegistration> CollectionTypeRegistrations { get; }
    }
}
