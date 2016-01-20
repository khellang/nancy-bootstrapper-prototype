using System;
using System.Linq;
using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public class TypeRegistrationFactory<TService, TDefaultImplementation> : ITypeRegistrationFactory<TService>
    {
        public TypeRegistrationFactory(Lifetime lifetime)
        {
            Lifetime = lifetime;
        }

        private Lifetime Lifetime { get; }

        private TypeRegistration Registration { get; set; }

        public void Use<TImplementation>() where TImplementation : TService
        {
            Use(typeof(TImplementation));
        }

        public void Use(Type implementationType)
        {
            Registration = new TypeRegistration(typeof(TService), implementationType, Lifetime);
        }

        public TypeRegistration GetRegistration(ITypeCatalog typeCatalog)
        {
            return Registration
                ?? ScanForCustomRegistration(typeCatalog, Lifetime)
                ?? GetDefaultRegistration(Lifetime);
        }

        private static TypeRegistration ScanForCustomRegistration(ITypeCatalog typeCatalog, Lifetime lifetime)
        {
            // TODO: Throw on multiple results?
            var customImplementationType = typeCatalog
                .GetTypesAssignableTo<TService>(ScanningStrategies.ExcludeNancy)
                .FirstOrDefault();

            if (customImplementationType != null)
            {
                return new TypeRegistration(typeof(TService), customImplementationType, lifetime);
            }

            return null;
        }

        private static TypeRegistration GetDefaultRegistration(Lifetime lifetime)
        {
            return new TypeRegistration(typeof(TService), typeof(TDefaultImplementation), lifetime);
        }
    }
}