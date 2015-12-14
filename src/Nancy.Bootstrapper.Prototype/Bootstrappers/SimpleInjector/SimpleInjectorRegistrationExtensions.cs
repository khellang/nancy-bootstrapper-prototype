using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Bootstrapper.Prototype.Cruft.Registration;
using SimpleInjector;
using Registration = SimpleInjector.Registration;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.SimpleInjector
{
    internal static class SimpleInjectorRegistrationExtensions
    {
        public static void Register(this Container container, IContainerRegistry registry)
        {
            container.RegisterTypes(registry.TypeRegistrations)
                .RegisterCollectionTypes(registry.CollectionTypeRegistrations)
                .RegisterInstances(registry.InstanceRegistrations);
        }

        private static Container RegisterTypes(this Container container, IEnumerable<TypeRegistration> registrations)
        {
            return registrations.Aggregate(container, Register);
        }

        private static Container RegisterCollectionTypes(this Container container, IEnumerable<CollectionTypeRegistration> registrations)
        {
            return registrations.Aggregate(container, Register);
        }

        private static Container RegisterInstances(this Container container, IEnumerable<InstanceRegistration> registrations)
        {
            return registrations.Aggregate(container, Register);
        }

        private static Container Register(this Container container, TypeRegistration registration)
        {
            container.Register(registration.ServiceType, registration.ImplementationType, registration.Lifetime.AsLifestyle());
            return container;
        }

        private static Container Register(this Container container, CollectionTypeRegistration registration)
        {
            var registrations = registration.ImplementationTypes
                .Select(implementationType => container.CreateRegistration(implementationType, registration.Lifetime));

            container.RegisterCollection(registration.ServiceType, registrations);

            return container;
        }

        private static Container Register(this Container container, InstanceRegistration registration)
        {
            container.RegisterSingleton(registration.ServiceType, registration.Instance);
            return container;
        }

        private static Lifestyle AsLifestyle(this Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton: return Lifestyle.Singleton;
                case Lifetime.Scoped: return Lifestyle.Scoped;
                case Lifetime.Transient: return Lifestyle.Transient;
                default: throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
            }
        }

        private static Registration CreateRegistration(this Container container, Type implementationType, Lifetime lifetime)
        {
            return lifetime.AsLifestyle().CreateRegistration(implementationType, container);
        }
    }
}