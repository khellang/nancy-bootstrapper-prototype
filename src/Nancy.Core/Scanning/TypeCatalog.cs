namespace Nancy.Core.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class TypeCatalog : ITypeCatalog
    {
        private readonly Lazy<IReadOnlyCollection<Assembly>> assemblies;

        public TypeCatalog(IAssemblyCatalog assemblyCatalog)
        {
            Check.NotNull(assemblyCatalog, nameof(assemblyCatalog));

            this.assemblies = new Lazy<IReadOnlyCollection<Assembly>>(() => assemblyCatalog.GetAssemblies().ToArray());
        }

        public IEnumerable<Type> GetTypesAssignableTo(Type targetType, ScanningStrategy strategy)
        {
            Check.NotNull(targetType, nameof(targetType));
            Check.NotNull(strategy, nameof(strategy));

            var targetTypeInfo = targetType.GetTypeInfo();

            foreach (var assembly in this.assemblies.Value)
            {
                if (!strategy.Invoke(assembly))
                {
                    continue;
                }

                foreach (var exportedType in assembly.ExportedTypes)
                {
                    var exportedTypeInfo = exportedType.GetTypeInfo();

                    if (exportedTypeInfo.IsAbstract)
                    {
                        continue;
                    }

                    if (exportedTypeInfo.IsAssignableTo(targetTypeInfo))
                    {
                        yield return exportedType;
                    }
                }
            }
        }
    }
}
