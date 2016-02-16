using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Nancy.Core.Scanning
{
    public class TypeCatalog : ITypeCatalog
    {
        public TypeCatalog(IAssemblyCatalog assemblyCatalog)
        {
            AssemblyCatalog = assemblyCatalog;
            AssemblyCache = new ConcurrentDictionary<ScanningStrategy, IReadOnlyCollection<Assembly>>();
        }

        private IAssemblyCatalog AssemblyCatalog { get; }

        private ConcurrentDictionary<ScanningStrategy, IReadOnlyCollection<Assembly>> AssemblyCache { get; }

        public IReadOnlyCollection<Type> GetTypesAssignableTo(Type targetType, ScanningStrategy strategy)
        {
            var result = new List<Type>();

            var assemblies = AssemblyCache.GetOrAdd(strategy, AssemblyCatalog, (s, c) => c.GetAssemblies(s));

            var targetTypeInfo = targetType.GetTypeInfo();

            foreach (var assembly in assemblies)
            {
                foreach (var exportedType in assembly.ExportedTypes)
                {
                    var exportedTypeInfo = exportedType.GetTypeInfo();

                    // TODO: If targetTypeInfo is a generic type definition, should we look for closed types?

                    if (!exportedTypeInfo.IsAbstract && targetTypeInfo.IsAssignableFrom(exportedTypeInfo))
                    {
                        result.Add(exportedType);
                    }
                }
            }

            return result.ToArray();
        }
    }
}