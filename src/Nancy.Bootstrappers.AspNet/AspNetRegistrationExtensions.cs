namespace Nancy.Bootstrappers.AspNet
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.Core.Registration;

    internal static class AspNetRegistrationExtensions
    {
        public static void AddRegistry(this IServiceCollection services, IContainerRegistry registry)
        {
            services.AddTypes(registry.TypeRegistrations);
            services.AddCollectionTypes(registry.CollectionTypeRegistrations);
            services.AddInstances(registry.InstanceRegistrations);
        }

        private static void AddTypes(this IServiceCollection services, IEnumerable<TypeRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                services.Add(registration);
            }
        }

        private static void AddCollectionTypes(this IServiceCollection services, IEnumerable<CollectionTypeRegistration> registrations)
        {
            foreach (var collectionTypeRegistration in registrations)
            {
                foreach (var implementationType in collectionTypeRegistration.ImplementationTypes)
                {
                    var registration = new TypeRegistration(
                        collectionTypeRegistration.ServiceType,
                        implementationType,
                        collectionTypeRegistration.Lifetime);

                    services.Add(registration);
                }
            }
        }

        private static void AddInstances(this IServiceCollection services, IEnumerable<InstanceRegistration> registrations)
        {
            foreach (var registration in registrations)
            {
                services.AddInstance(registration.ServiceType, registration.Instance);
            }
        }

        private static void Add(this IServiceCollection services, TypeRegistration registration)
        {
            switch (registration.Lifetime)
            {
                case Lifetime.Singleton:
                    services.AddSingleton(registration.ServiceType, registration.ImplementationType);
                    break;
                case Lifetime.PerRequest:
                    services.AddScoped(registration.ServiceType, registration.ImplementationType);
                    break;
                case Lifetime.Transient:
                    services.AddTransient(registration.ServiceType, registration.ImplementationType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(registration.Lifetime), "Invalid lifetime.");
            }
        }
    }
}
