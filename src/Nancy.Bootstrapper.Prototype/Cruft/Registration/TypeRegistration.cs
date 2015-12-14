using System;

namespace Nancy.Bootstrapper.Prototype.Cruft.Registration
{
    public class TypeRegistration : Registration
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
    }
}