using System;
using System.Collections.Generic;

namespace Nancy.Core.Registration
{
    public class CollectionTypeRegistration : ContainerRegistration
    {
        public CollectionTypeRegistration(Type serviceType, IReadOnlyCollection<Type> implementationTypes, Lifetime lifetime)
            : base(serviceType, lifetime)
        {
            ImplementationTypes = implementationTypes;
        }

        public IReadOnlyCollection<Type> ImplementationTypes { get; }
    }
}