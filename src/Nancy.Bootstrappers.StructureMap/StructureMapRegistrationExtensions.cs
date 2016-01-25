using System;
using System.Collections.Generic;
using Nancy.Core;
using Nancy.Core.Registration;
using StructureMap;
using StructureMap.Pipeline;

namespace Nancy.Bootstrappers.StructureMap
{
    public static class StructureMapRegistrationExtensions
    {
        public static void AddNancy(this IContainer builder,
            IBootstrapper<IContainer, IContainer> bootstrapper)
        {
            bootstrapper.Populate(builder);
        }

        internal static void AddRegistry(this IProfileRegistry profileRegistry, IContainerRegistry registry)
        {
            profileRegistry.RegisterTypes(registry.TypeRegistrations);
            profileRegistry.RegisterCollectionTypes(registry.CollectionTypeRegistrations);
            profileRegistry.RegisterInstances(registry.InstanceRegistrations);
        }

        private static void RegisterTypes(this IProfileRegistry registry, IEnumerable<TypeRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                registry.Register(registration);
            }
        }

        private static void RegisterCollectionTypes(this IProfileRegistry registry, IEnumerable<CollectionTypeRegistration> registrations)
        {
            foreach (var collectionRegistration in registrations)
            {
                foreach (var implementationType in collectionRegistration.ImplementationTypes)
                {
                    var registration = new TypeRegistration(
                        collectionRegistration.ServiceType,
                        implementationType,
                        collectionRegistration.Lifetime);

                    registry.Register(registration);
                }
            }
        }

        private static void Register(this IProfileRegistry registry, TypeRegistration registration)
        {
            registry.For(registration.ServiceType)
                .Use(registration.ImplementationType)
                .LifecycleIs(registration.Lifetime);
        }

        private static void RegisterInstances(this IProfileRegistry registry, IEnumerable<InstanceRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                registry.For(registration.ServiceType).Use(registration.Instance);
            }
        }

        private static void LifecycleIs(this ConfiguredInstance instance, Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                    instance.LifecycleIs<SingletonLifecycle>();
                    break;
                case Lifetime.PerRequest:
                    instance.LifecycleIs<ContainerLifecycle>();
                    break;
                case Lifetime.Transient:
                    instance.LifecycleIs<UniquePerRequestLifecycle>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Invalid lifetime.");
            }
        }
    }
}