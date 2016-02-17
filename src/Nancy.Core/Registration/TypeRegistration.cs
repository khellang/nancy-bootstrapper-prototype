namespace Nancy.Core.Registration
{
    using System;

    public class TypeRegistration : ContainerRegistration
    {
        public TypeRegistration(Type serviceType, Type implementationType, Lifetime lifetime)
            : base(serviceType, lifetime)
        {
            this.ImplementationType = implementationType;
        }

        public Type ImplementationType { get; }
    }
}
