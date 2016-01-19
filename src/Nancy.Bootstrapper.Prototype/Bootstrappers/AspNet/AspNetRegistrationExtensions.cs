using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Bootstrapper.Prototype.Registration;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.AspNet
{
    public static class AspNetRegistrationExtensions
    {
        public static void AddRegistry(this IServiceCollection services, IContainerRegistry registry)
        {
            services.AddTypes(registry.TypeRegistrations)
                .AddCollectionTypes(registry.CollectionTypeRegistrations)
                .AddInstances(registry.InstanceRegistrations);
        }

        private static IServiceCollection AddTypes(this IServiceCollection services, IEnumerable<TypeRegistration> registrations)
        {
            return registrations.Aggregate(services, Add);
        }

        private static IServiceCollection AddCollectionTypes(this IServiceCollection services, IEnumerable<CollectionTypeRegistration> registrations)
        {
            return registrations.Aggregate(services, Add);
        }

        private static IServiceCollection AddInstances(this IServiceCollection services, IEnumerable<InstanceRegistration> registrations)
        {
            return registrations.Aggregate(services, Add);
        }

        private static IServiceCollection Add(this IServiceCollection services, TypeRegistration registration)
        {
            switch (registration.Lifetime)
            {
                case Lifetime.Singleton:
                    return services.AddSingleton(registration.ServiceType, registration.ImplementationType);
                case Lifetime.Scoped:
                    return services.AddScoped(registration.ServiceType, registration.ImplementationType);
                case Lifetime.Transient:
                    return services.AddTransient(registration.ServiceType, registration.ImplementationType);
                default:
                    throw new ArgumentOutOfRangeException(nameof(registration.Lifetime), "Invalid lifetime.");
            }
        }

        private static IServiceCollection Add(this IServiceCollection services, CollectionTypeRegistration registration)
        {
            return registration.ImplementationTypes
                .Aggregate(services, (serviceCollection, implementationType) =>
                    serviceCollection.Add(CreateRegistration(registration, implementationType)));
        }

        private static IServiceCollection Add(this IServiceCollection services, InstanceRegistration registration)
        {
            return services.AddInstance(registration.ServiceType, registration.Instance);
        }

        private static TypeRegistration CreateRegistration(ContainerRegistration registration, Type implementationType)
        {
            return new TypeRegistration(registration.ServiceType, implementationType, registration.Lifetime);
        }
    }
}