using System;

namespace Nancy.Bootstrapper.Prototype.Registration
{
    public class TypeRegistration : ContainerRegistration
    {
        public TypeRegistration(Type serviceType, Type implementationType, Lifetime lifetime)
            : base(serviceType, lifetime)
        {
            ImplementationType = implementationType;
        }

        public Type ImplementationType { get; }

        public override string ToString()
        {
            return $"{Lifetime} - {ServiceType.Name} -> {ImplementationType.Name}";
        }
    }
}