using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Nancy.Core.Scanning
{
    public class LibraryManagerAssemblyCatalog : IAssemblyCatalog
    {
        private static readonly string NancyLibraryName = ScanningStrategies.NancyAssemblyName;

        public LibraryManagerAssemblyCatalog(ILibraryManager libraryManager)
        {
            LibraryManager = libraryManager;
        }

        private ILibraryManager LibraryManager { get; }

        public IReadOnlyCollection<Assembly> GetAssemblies(ScanningStrategy strategy)
        {
            var assemblies = new HashSet<Assembly>();

            var nancyLibrary = LibraryManager.GetLibrary(NancyLibraryName);

            foreach (var assemblyName in nancyLibrary.Assemblies)
            {
                if (strategy.Invoke(assemblyName))
                {
                    assemblies.Add(Assembly.Load(assemblyName));
                }
            }

            var referencingLibraries = LibraryManager.GetReferencingLibraries(NancyLibraryName);

            foreach (var referencingLibrary in referencingLibraries)
            {
                foreach (var assemblyName in referencingLibrary.Assemblies)
                {
                    if (strategy.Invoke(assemblyName))
                    {
                        assemblies.Add(Assembly.Load(assemblyName));
                    }
                }
            }

            return assemblies.ToArray();
        }
    }
}