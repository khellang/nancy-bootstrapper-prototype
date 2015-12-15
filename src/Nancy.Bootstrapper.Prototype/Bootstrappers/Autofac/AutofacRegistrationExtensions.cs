using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Nancy.Bootstrapper.Prototype.Registration;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.Autofac
{
    internal static class AutofacRegistrationExtensions
    {
        public static void Register(this ContainerBuilder builder, IContainerRegistry registry)
        {
            builder.RegisterTypes(registry.TypeRegistrations)
                .RegisterCollectionTypes(registry.CollectionTypeRegistrations)
                .RegisterInstances(registry.InstanceRegistrations);
        }

        private static ContainerBuilder RegisterTypes(this ContainerBuilder builder, IEnumerable<TypeRegistration> registrations)
        {
            return registrations.Aggregate(builder, Register);
        }

        private static ContainerBuilder RegisterCollectionTypes(this ContainerBuilder builder, IEnumerable<CollectionTypeRegistration> registrations)
        {
            return registrations.SelectMany(registration =>
                registration.ImplementationTypes
                    .Select(registration.AsTypeRegistration))
                .Aggregate(builder, Register);
        }

        private static ContainerBuilder RegisterInstances(this ContainerBuilder builder, IEnumerable<InstanceRegistration> registrations)
        {
            return registrations.Aggregate(builder, Register);
        }

        private static ContainerBuilder Register(this ContainerBuilder builder, TypeRegistration registration)
        {
            builder.RegisterType(registration.ImplementationType)
                .As(registration.ServiceType)
                .WithLifetime(registration.Lifetime)
                .PreserveExistingDefaults();

            return builder;
        }

        private static ContainerBuilder Register(this ContainerBuilder builder, InstanceRegistration registration)
        {
            builder.RegisterInstance(registration.Instance)
                .As(registration.ServiceType)
                .PreserveExistingDefaults();

            return builder;
        }

        private static IRegistrationBuilder<T, TActivatorData, TRegistrationStyle> WithLifetime<T, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<T, TActivatorData, TRegistrationStyle> builder, Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton: return builder.SingleInstance();
                case Lifetime.Scoped: return builder.InstancePerLifetimeScope();
                case Lifetime.Transient: return builder.InstancePerDependency();
                default: throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Invalid lifetime.");
            }
        }

        private static TypeRegistration AsTypeRegistration(this ContainerRegistration registration, Type implementationType)
        {
            return new TypeRegistration(registration.ServiceType, implementationType, registration.Lifetime);
        }
    }
}