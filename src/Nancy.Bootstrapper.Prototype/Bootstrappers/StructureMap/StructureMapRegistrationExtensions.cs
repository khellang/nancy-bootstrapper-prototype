using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication7.Cruft.Registration;
using StructureMap;
using StructureMap.Pipeline;

namespace ConsoleApplication7.Bootstrappers.StructureMap
{
    internal static class StructureMapRegistrationExtensions
    {
        public static void AddRegistry(this ConfigurationExpression config, IContainerRegistry registry)
        {
            config.RegisterTypes(registry.TypeRegistrations)
                .RegisterCollectionTypes(registry.CollectionTypeRegistrations)
                .RegisterInstances(registry.InstanceRegistrations);
        }

        private static ConfigurationExpression RegisterTypes(this ConfigurationExpression config, IEnumerable<TypeRegistration> registrations)
        {
            return registrations.Aggregate(config, Register);
        }

        private static ConfigurationExpression RegisterCollectionTypes(this ConfigurationExpression config, IEnumerable<CollectionTypeRegistration> registrations)
        {
            return registrations.SelectMany(registration =>
                registration.ImplementationTypes
                    .Select(registration.AsTypeRegistration))
                .Aggregate(config, Register);
        }

        private static ConfigurationExpression RegisterInstances(this ConfigurationExpression config, IEnumerable<InstanceRegistration> registrations)
        {
            return registrations.Aggregate(config, Register);
        }

        private static ConfigurationExpression Register(this ConfigurationExpression config, TypeRegistration registration)
        {
            config.For(registration.ServiceType)
                .Use(registration.ImplementationType)
                .LifecycleIs(registration.Lifetime);

            return config;
        }

        private static ConfigurationExpression Register(this ConfigurationExpression config, InstanceRegistration registration)
        {
            config.For(registration.ServiceType).Use(registration.Instance);
            return config;
        }

        private static ConfiguredInstance LifecycleIs(this ConfiguredInstance config, Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton: return config.LifecycleIs<SingletonLifecycle>();
                case Lifetime.Scoped: return config.LifecycleIs<ContainerLifecycle>();
                case Lifetime.Transient: return config.LifecycleIs<UniquePerRequestLifecycle>();
                default: throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Invalid lifetime.");
            }
        }

        private static TypeRegistration AsTypeRegistration(this Registration registration, Type implementationType)
        {
            return new TypeRegistration(registration.ServiceType, implementationType, registration.Lifetime);
        }
    }
}