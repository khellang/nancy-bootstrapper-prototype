using System;
using System.Collections.Generic;
using Nancy.Bootstrapper.Prototype.Configuration;
using Nancy.Bootstrapper.Prototype.Registration;
using System.Linq;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public static class TypeCatalogExtensions
    {
        public static IEnumerable<Type> GetTypesAssignableTo<T>(this ITypeCatalog typeCatalog)
        {
            return typeCatalog.GetTypesAssignableTo(typeof(T));
        }

        public static IEnumerable<Type> GetTypesAssignableTo(this ITypeCatalog typeCatalog, Type type)
        {
            return typeCatalog.GetTypesAssignableTo(type, ScanMode.All);
        }

        public static IEnumerable<Type> GetTypesAssignableTo<T>(this ITypeCatalog typeCatalog, ScanMode scanMode)
        {
            return typeCatalog.GetTypesAssignableTo(typeof(T), scanMode);
        }

        public static IReadOnlyCollection<TRegistration> GetRegistrations<TRegistration>(this ITypeCatalog typeCatalog,
            IEnumerable<IRegistrationFactory<TRegistration>> factories)
            where TRegistration : ContainerRegistration
        {
            return factories.Select(factory => factory.GetRegistration(typeCatalog)).ToArray();
        }
    }
}