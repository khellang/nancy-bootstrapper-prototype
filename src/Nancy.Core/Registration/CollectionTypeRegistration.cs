namespace Nancy.Core.Registration
{
    using System;
    using System.Collections.Generic;

    public class CollectionTypeRegistration : ContainerRegistration
    {
        public CollectionTypeRegistration(Type serviceType, IReadOnlyCollection<Type> implementationTypes, Lifetime lifetime)
            : base(serviceType, lifetime)
        {
            this.ImplementationTypes = implementationTypes;
        }

        public IReadOnlyCollection<Type> ImplementationTypes { get; }
    }
}
