using System;
using System.Collections.Generic;
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

        public IReadOnlyCollection<Type> TypesOf(Type targetType, ScanMode scanMode)
        {
            var result = new List<Type>();

            var assemblies = AssemblyCatalog.GetAssemblies(scanMode);

            foreach (var assembly in assemblies)
            {
                foreach (var exportedType in assembly.ExportedTypes)
                {
                    if (IsCandidate(exportedType, targetType))
                    {
                        result.Add(exportedType);
                    }
                }
            }

            return result.ToArray();
        }

        private static bool IsCandidate(Type type, Type targetType)
        {
            var typeInfo = type.GetTypeInfo();
            var targetTypeInfo = targetType.GetTypeInfo();

            return !typeInfo.IsAbstract && targetTypeInfo.IsAssignableFrom(typeInfo);
        }
    }
}