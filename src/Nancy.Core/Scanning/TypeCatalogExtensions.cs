namespace Nancy.Core.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Nancy.Core.Configuration;
    using Nancy.Core.Registration;

    [DebuggerStepThrough]
    public static class TypeCatalogExtensions
    {
        public static Type GetFirstTypeAssignableTo<T>(this ITypeCatalog typeCatalog)
        {
            return typeCatalog.GetFirstTypeAssignableTo(typeof(T));
        }

        public static Type GetFirstTypeAssignableTo(this ITypeCatalog typeCatalog, Type type)
        {
            return typeCatalog.GetFirstTypeAssignableTo(type, ScanningStrategies.All);
        }

        public static Type GetFirstTypeAssignableTo(this ITypeCatalog typeCatalog, Type type, ScanningStrategy strategy)
        {
            return typeCatalog.GetTypesAssignableTo(type, strategy).FirstOrDefault();
        }

        public static IEnumerable<Type> GetTypesAssignableTo<T>(this ITypeCatalog typeCatalog)
        {
            return typeCatalog.GetTypesAssignableTo(typeof(T));
        }

        public static IEnumerable<Type> GetTypesAssignableTo(this ITypeCatalog typeCatalog, Type type)
        {
            return typeCatalog.GetTypesAssignableTo(type, ScanningStrategies.All);
        }

        public static IEnumerable<Type> GetTypesAssignableTo<T>(this ITypeCatalog typeCatalog, ScanningStrategy strategy)
        {
            return typeCatalog.GetTypesAssignableTo(typeof(T), strategy);
        }

        internal static IReadOnlyCollection<TRegistration> GetRegistrations<TRegistration>(
            this ITypeCatalog typeCatalog,
            IReadOnlyCollection<IRegistrationFactory<TRegistration>> factories)
            where TRegistration : ContainerRegistration
        {
            Check.NotNull(typeCatalog, nameof(typeCatalog));
            Check.NotNull(factories, nameof(factories));

            var registrations = new List<TRegistration>(capacity: factories.Count);

            foreach (var factory in factories)
            {
                registrations.Add(factory.GetRegistration(typeCatalog));
            }

            return registrations;
        }
    }
}
