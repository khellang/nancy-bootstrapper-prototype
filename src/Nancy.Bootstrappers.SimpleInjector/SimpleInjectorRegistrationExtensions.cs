namespace Nancy.Bootstrappers.SimpleInjector
{
    using System;
    using System.Collections.Generic;
    using global::SimpleInjector;
    using Nancy.Core.Registration;

    internal static class SimpleInjectorRegistrationExtensions
    {
        public static void Register(this Container container, IContainerRegistry registry)
        {
            container.RegisterTypes(registry.TypeRegistrations);
            container.RegisterCollectionTypes(registry.CollectionTypeRegistrations);
            container.RegisterInstances(registry.InstanceRegistrations);
        }

        private static void RegisterTypes(this Container container, IEnumerable<TypeRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                container.Register(registration.ServiceType, registration.ImplementationType, registration.Lifetime.AsLifestyle());
            }
        }

        private static void RegisterCollectionTypes(this Container container, IEnumerable<CollectionTypeRegistration> registrations)
        {
            foreach (var collectionTypeRegistration in registrations)
            {
                var capacity = collectionTypeRegistration.ImplementationTypes.Count;

                var convertedRegistrations = new List<Registration>(capacity);

                var lifestyle = collectionTypeRegistration.Lifetime.AsLifestyle();

                foreach (var implementationType in collectionTypeRegistration.ImplementationTypes)
                {
                    var converted = lifestyle.CreateRegistration(
                        collectionTypeRegistration.ServiceType,
                        implementationType,
                        container);

                    convertedRegistrations.Add(converted);
                }

                container.RegisterCollection(collectionTypeRegistration.ServiceType, convertedRegistrations);
            }
        }

        private static void RegisterInstances(this Container container, IEnumerable<InstanceRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                container.RegisterSingleton(registration.ServiceType, registration.Instance);
            }
        }

        private static Lifestyle AsLifestyle(this Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                    return Lifestyle.Singleton;
                case Lifetime.PerRequest:
                    return Lifestyle.Scoped;
                case Lifetime.Transient:
                    return Lifestyle.Transient;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Invalid lifetime.");
            }
        }
    }
}
