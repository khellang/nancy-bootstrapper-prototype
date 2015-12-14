using System;
using System.Collections.Generic;

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
    }
}