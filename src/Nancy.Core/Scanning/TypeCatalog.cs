namespace Nancy.Core.Scanning
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;

    public class TypeCatalog : ITypeCatalog
    {
        private readonly ConcurrentDictionary<ScanningStrategy, IReadOnlyCollection<Assembly>> assemblyCache;

        private readonly IAssemblyCatalog assemblyCatalog;

        public TypeCatalog(IAssemblyCatalog assemblyCatalog)
        {
            this.assemblyCatalog = assemblyCatalog;
            this.assemblyCache = new ConcurrentDictionary<ScanningStrategy, IReadOnlyCollection<Assembly>>();
        }

        public IReadOnlyCollection<Type> GetTypesAssignableTo(Type targetType, ScanningStrategy strategy)
        {
            var result = new List<Type>();

            var assemblies = this.assemblyCache.GetOrAdd(strategy, this.assemblyCatalog, (s, c) => c.GetAssemblies(s));

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
