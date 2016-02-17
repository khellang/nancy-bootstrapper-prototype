namespace Nancy.Core.Scanning
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.PlatformAbstractions;

    public class LibraryManagerAssemblyCatalog : IAssemblyCatalog
    {
        private static readonly string NancyLibraryName = ScanningStrategies.NancyAssemblyName;

        private readonly ILibraryManager libraryManager;

        public LibraryManagerAssemblyCatalog(ILibraryManager libraryManager)
        {
            this.libraryManager = libraryManager;
        }

        public IReadOnlyCollection<Assembly> GetAssemblies(ScanningStrategy strategy)
        {
            var assemblies = new HashSet<Assembly>();

            var nancyLibrary = this.libraryManager.GetLibrary(NancyLibraryName);

            foreach (var assemblyName in nancyLibrary.Assemblies)
            {
                if (strategy.Invoke(assemblyName))
                {
                    assemblies.Add(Assembly.Load(assemblyName));
                }
            }

            var referencingLibraries = this.libraryManager.GetReferencingLibraries(NancyLibraryName);

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
