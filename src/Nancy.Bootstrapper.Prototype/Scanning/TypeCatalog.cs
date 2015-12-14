using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public class TypeCatalog : ITypeCatalog
    {
        public TypeCatalog(IAssemblyCatalog assemblyCatalog)
        {
            AssemblyCatalog = assemblyCatalog;
        }

        private IAssemblyCatalog AssemblyCatalog { get; }

        public IEnumerable<Type> TypesOf(Type targetType, ScanMode scanMode)
        {
            return AssemblyCatalog.GetAssemblies(scanMode)
                .SelectMany(assembly => assembly.ExportedTypes)
                .Where(type => IsCandidate(type, targetType))
                .ToArray();
        }

        private static bool IsCandidate(Type type, Type targetType)
        {
            var typeInfo = type.GetTypeInfo();
            var targetTypeInfo = targetType.GetTypeInfo();

            return !typeInfo.IsAbstract && targetTypeInfo.IsAssignableFrom(typeInfo);
        }
    }
}