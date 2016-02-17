namespace Nancy.Core.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class TypeCatalog : ITypeCatalog
    {
        private readonly IAssemblyCatalog assemblyCatalog;

        public TypeCatalog(IAssemblyCatalog assemblyCatalog)
        {
            this.assemblyCatalog = assemblyCatalog;
        }

        public IReadOnlyCollection<Type> GetTypesAssignableTo(Type targetType, ScanningStrategy strategy)
        {
            var assemblies = this.assemblyCatalog.GetAssemblies();

            var result = new List<Type>();

            var targetTypeInfo = targetType.GetTypeInfo();

            foreach (var assembly in assemblies)
            {
                if (!strategy.Invoke(assembly))
                {
                    continue;
                }

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
