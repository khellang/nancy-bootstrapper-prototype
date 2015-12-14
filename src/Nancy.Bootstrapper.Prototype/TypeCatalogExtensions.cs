using System;
using System.Collections.Generic;

namespace Nancy.Bootstrapper.Prototype
{
    public static class TypeCatalogExtensions
    {
        public static IEnumerable<Type> TypesOf<T>(this ITypeCatalog typeCatalog)
        {
            return typeCatalog.TypesOf(typeof(T));
        }

        public static IEnumerable<Type> TypesOf(this ITypeCatalog typeCatalog, Type type)
        {
            return typeCatalog.TypesOf(type, ScanMode.All);
        }

        public static IEnumerable<Type> TypesOf<T>(this ITypeCatalog typeCatalog, ScanMode scanMode)
        {
            return typeCatalog.TypesOf(typeof(T), scanMode);
        }
    }
}