using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Bootstrapper.Prototype
{
    public class TypeCatalog : ITypeCatalog
    {
        public TypeCatalog(IAssemblyCatalog assemblyCatalog)
        {
            AssemblyCatalog = assemblyCatalog;
        }

        private IAssemblyCatalog AssemblyCatalog { get; }

        public IEnumerable<Type> TypesOf(Type type, ScanMode scanMode)
        {
            return AssemblyCatalog.Assemblies
                .SelectMany(x => x.ExportedTypes)
                .Where(x => !x.IsAbstract && type.IsAssignableFrom(x))
                .ToArray();
        }
    }
}