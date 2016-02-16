using System;

namespace Nancy.Core.Registration
{
    public class TypeRegistration : ContainerRegistration
    {
        public TypeRegistration(Type serviceType, Type implementationType, Lifetime lifetime)
            : base(serviceType, lifetime)
        {
            ImplementationType = implementationType;
        }

        public Type ImplementationType { get; }
    }
}