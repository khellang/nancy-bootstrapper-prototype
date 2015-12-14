using System;
using System.Collections.Generic;

namespace Nancy.Bootstrapper.Prototype.Cruft.Registration
{
    public class CollectionTypeRegistration : Registration
    {
        public CollectionTypeRegistration(Type serviceType, IReadOnlyCollection<Type> implementationTypes, Lifetime lifetime)
            : base(serviceType, lifetime)
        {
            ImplementationTypes = implementationTypes;
        }

        public IReadOnlyCollection<Type> ImplementationTypes { get; }
    }
}