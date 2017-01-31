namespace Nancy.Bootstrappers.Autofac
{
    using System;
    using System.Collections.Generic;
    using global::Autofac;
    using global::Autofac.Builder;
    using Nancy.Core.Registration;

    internal static class AutofacRegistrationExtensions
    {
        public static void Register(this ContainerBuilder builder, IContainerRegistry registry)
        {
            builder.RegisterTypes(registry.TypeRegistrations);
            builder.RegisterCollectionTypes(registry.CollectionTypeRegistrations);
            builder.RegisterInstances(registry.InstanceRegistrations);
        }

        private static void RegisterTypes(this ContainerBuilder builder, IEnumerable<TypeRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                builder.Register(registration);
            }
        }

        private static void RegisterCollectionTypes(this ContainerBuilder builder, IEnumerable<CollectionTypeRegistration> registrations)
        {
            foreach (var collectionTypeRegistration in registrations)
            {
                foreach (var implementationType in collectionTypeRegistration.ImplementationTypes)
                {
                    var registration = new TypeRegistration(
                        collectionTypeRegistration.ServiceType,
                        implementationType,
                        collectionTypeRegistration.Lifetime);

                    builder.Register(registration);
                }
            }
        }

        private static void RegisterInstances(this ContainerBuilder builder, IEnumerable<InstanceRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                builder.RegisterInstance(registration.Instance)
                    .As(registration.ServiceType)
                    .PreserveExistingDefaults();
            }
        }

        private static void Register(this ContainerBuilder builder, TypeRegistration registration)
        {
            builder.RegisterType(registration.ImplementationType)
                .As(registration.ServiceType)
                .WithLifetime(registration.Lifetime)
                .PreserveExistingDefaults();
        }

        private static IRegistrationBuilder<T, TActivatorData, TRegistrationStyle> WithLifetime<T, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<T, TActivatorData, TRegistrationStyle> builder,
            Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                    return builder.SingleInstance();
                case Lifetime.PerRequest:
                    return builder.InstancePerRequest();
                case Lifetime.Transient:
                    return builder.InstancePerDependency();
                default:
                    throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Invalid lifetime.");
            }
        }
    }
}
