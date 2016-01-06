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

        public IReadOnlyCollection<Type> GetTypesAssignableTo(Type targetType, ScanningStrategy strategy)
        {
            var result = new List<Type>();

            var assemblies = AssemblyCatalog.GetAssemblies(strategy);

            var targetTypeInfo = targetType.GetTypeInfo();

            foreach (var assembly in assemblies)
            {
                foreach (var exportedType in assembly.ExportedTypes)
                {
                    var exportedTypeInfo = exportedType.GetTypeInfo();

                    if (exportedTypeInfo.IsAbstract)
                    {
                        continue;
                    }

                    if (!targetTypeInfo.IsAssignableFrom(exportedTypeInfo))
                    {
                        continue;
                    }

                    result.Add(exportedType);
                }
            }

            return result.ToArray();
        }
    }
}