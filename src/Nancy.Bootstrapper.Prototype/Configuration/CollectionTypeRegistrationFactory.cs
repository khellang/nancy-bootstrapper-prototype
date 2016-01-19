using System;
using System.Collections.Generic;
using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public class CollectionTypeRegistrationFactory<TService> : ICollectionTypeRegistrationFactory<TService>
    {
        public CollectionTypeRegistrationFactory(Lifetime lifetime, IReadOnlyCollection<Type> defaultImplementationTypes)
        {
            Lifetime = lifetime;
            DefaultImplementationTypes = defaultImplementationTypes;
            ImplementationTypes = new List<Type>();
        }

        private Lifetime Lifetime { get; }

        private IReadOnlyCollection<Type> DefaultImplementationTypes { get; }

        private List<Type> ImplementationTypes { get; }

        public void Use<TImplementation>() where TImplementation : TService
        {
            Use(typeof(TImplementation));
        }

        public void Use(Type implementationType)
        {
            ImplementationTypes.Add(implementationType);
        }

        public CollectionTypeRegistration GetRegistration(ITypeCatalog typeCatalog)
        {
            return GetRegistration(ImplementationTypes, Lifetime)
                ?? ScanForCustomImplementations(typeCatalog, Lifetime)
                ?? GetDefaultRegistration(DefaultImplementationTypes, Lifetime);
        }

        private static CollectionTypeRegistration GetRegistration(IReadOnlyCollection<Type> implementationTypes, Lifetime lifetime)
        {
            if (implementationTypes.Count > 0)
            {
                return new CollectionTypeRegistration(typeof(TService), implementationTypes, lifetime);
            }

            return null;
        }

        private static CollectionTypeRegistration ScanForCustomImplementations(ITypeCatalog typeCatalog, Lifetime lifetime)
        {
            var customImplementationTypes = typeCatalog
                .GetTypesAssignableTo<TService>(ScanningStrategies.ExcludeNancy);

            if (customImplementationTypes.Count > 0)
            {
                return new CollectionTypeRegistration(typeof(TService), customImplementationTypes, lifetime);
            }

            return null;
        }

        private static CollectionTypeRegistration GetDefaultRegistration(IReadOnlyCollection<Type> defaultImplementationTypes, Lifetime lifetime)
        {
            return new CollectionTypeRegistration(typeof(TService), defaultImplementationTypes, lifetime);
        }
    }
}