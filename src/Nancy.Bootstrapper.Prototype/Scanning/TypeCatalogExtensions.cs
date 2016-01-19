using System;
using System.Collections.Generic;
using Nancy.Bootstrapper.Prototype.Configuration;
using Nancy.Bootstrapper.Prototype.Registration;
using System.Linq;

namespace Nancy.Bootstrapper.Prototype.Scanning
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

        public static IReadOnlyCollection<TRegistration> GetRegistrations<TRegistration>(this ITypeCatalog typeCatalog,
            IEnumerable<IRegistrationFactory<TRegistration>> factories)
            where TRegistration : ContainerRegistration
        {
            return factories.Select(factory => factory.GetRegistration(typeCatalog)).ToArray();
        }
    }
}