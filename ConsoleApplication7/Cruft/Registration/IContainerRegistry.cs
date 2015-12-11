using System.Collections.Generic;

namespace ConsoleApplication7.Cruft.Registration
{
    public interface IContainerRegistry
    {
        IReadOnlyCollection<TypeRegistration> TypeRegistrations { get; }
        
        IReadOnlyCollection<InstanceRegistration> InstanceRegistrations { get; }
        
        IReadOnlyCollection<CollectionTypeRegistration> CollectionTypeRegistrations { get; }    
    }
}