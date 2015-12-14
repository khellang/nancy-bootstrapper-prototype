using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Bootstrapper.Prototype.Cruft
{
    public class DefaultTypeCatalog : ITypeCatalog
    {
        public DefaultTypeCatalog(IAssemblyCatalog assemblyCatalog)
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