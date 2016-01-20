using System;
using System.Collections.Generic;
using Nancy.Core.Configuration;
using Nancy.Core.Registration;

namespace Nancy.Core.Scanning
{
    public static class TypeCatalogExtensions
    {
        public static IReadOnlyCollection<Type> GetTypesAssignableTo<T>(this ITypeCatalog typeCatalog)
        {
            return typeCatalog.GetTypesAssignableTo(typeof(T));
        }

        public static IReadOnlyCollection<Type> GetTypesAssignableTo(this ITypeCatalog typeCatalog, Type type)
        {
            return typeCatalog.GetTypesAssignableTo(type, ScanningStrategies.All);
        }

        public static IReadOnlyCollection<Type> GetTypesAssignableTo<T>(this ITypeCatalog typeCatalog, ScanningStrategy strategy)
        {
            return typeCatalog.GetTypesAssignableTo(typeof(T), strategy);
        }

        internal static IReadOnlyCollection<TRegistration> GetRegistrations<TRegistration>(this ITypeCatalog typeCatalog,
            IReadOnlyCollection<IRegistrationFactory<TRegistration>> factories)
            where TRegistration : ContainerRegistration
        {
            var registrations = new List<TRegistration>(capacity: factories.Count);

            foreach (var factory in factories)
            {
                registrations.Add(factory.GetRegistration(typeCatalog));
            }

            return registrations;
        }
    }
}