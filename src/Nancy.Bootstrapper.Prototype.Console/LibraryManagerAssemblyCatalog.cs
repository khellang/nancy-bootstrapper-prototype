using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class LibraryManagerAssemblyCatalog : IAssemblyCatalog
    {
        public LibraryManagerAssemblyCatalog(ILibraryManager libraryManager)
        {
            LibraryManager = libraryManager;
        }

        private ILibraryManager LibraryManager { get; }

        public IEnumerable<Assembly> Assemblies =>
            LibraryManager.GetReferencingLibraries("Nancy.Bootstrapper.Prototype")
                .SelectMany(x => x.Assemblies)
                .Select(Assembly.Load);
    }
}