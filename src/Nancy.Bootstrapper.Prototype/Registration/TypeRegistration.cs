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

        public static TypeRegistration Create<TService, TImplementation>(Lifetime lifetime)
            where TImplementation : TService
        {
            return Create<TService>(typeof(TImplementation), lifetime);
        }

        public static TypeRegistration Create<TService>(Type implementationType, Lifetime lifetime)
        {
            return new TypeRegistration(typeof(TService), implementationType, lifetime);
        }

        public override string ToString()
        {
            return $"{Lifetime} - {ServiceType.Name} -> {ImplementationType.Name}";
        }
    }
}