using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Bootstrapper.Prototype.Registration
{
    public class CollectionTypeRegistration : ContainerRegistration
    {
        public CollectionTypeRegistration(Type serviceType, IReadOnlyCollection<Type> implementationTypes, Lifetime lifetime)
            : base(serviceType, lifetime)
        {
            ImplementationTypes = implementationTypes;
        }

        public IReadOnlyCollection<Type> ImplementationTypes { get; }

        public override string ToString()
        {
            return $"{Lifetime} - {ServiceType.Name} -> {string.Join(", ", ImplementationTypes.Select(t => t.Name))}";
        }
    }
}