using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public class LibraryManagerAssemblyCatalog : IAssemblyCatalog
    {
        public LibraryManagerAssemblyCatalog(ILibraryManager libraryManager)
        {
            LibraryManager = libraryManager;
        }

        private ILibraryManager LibraryManager { get; }

        public IReadOnlyCollection<Assembly> GetAssemblies(ScanMode scanMode)
        {
            var assemblies = new List<Assembly>();

            if (scanMode != ScanMode.ExcludeNancy)
            {
                var nancy = LibraryManager.GetLibrary("Nancy.Bootstrapper.Prototype");

                foreach (var assemblyName in nancy.Assemblies)
                {
                    assemblies.Add(Assembly.Load(assemblyName));
                }

                if (scanMode == ScanMode.OnlyNancy)
                {
                    return assemblies;
                }
            }

            var referencingLibraries = LibraryManager.GetReferencingLibraries("Nancy.Bootstrapper.Prototype");

            foreach (var referencingLibrary in referencingLibraries)
            {
                foreach (var assemblyName in referencingLibrary.Assemblies)
                {
                    assemblies.Add(Assembly.Load(assemblyName));
                }
            }

            return assemblies.ToArray();
        }
    }
}