using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class LibraryManagerAssemblyCatalog : IAssemblyCatalog
    {
        public LibraryManagerAssemblyCatalog(ILibraryManager libraryManager)
        {
            LibraryManager = libraryManager;
        }

        private ILibraryManager LibraryManager { get; }

        public IEnumerable<Assembly> GetAssemblies(ScanMode scanMode)
        {
            if (scanMode != ScanMode.ExcludeNancy)
            {
                var nancy = LibraryManager.GetLibrary("Nancy.Bootstrapper.Prototyp");

                foreach (var assemblyName in nancy.Assemblies)
                {
                    yield return Assembly.Load(assemblyName);
                }

                if (scanMode == ScanMode.OnlyNancy)
                {
                    yield break;
                }
            }

            var referencingLibraries = LibraryManager.GetReferencingLibraries("Nancy.Bootstrapper.Prototype");

            foreach (var referencingLibrary in referencingLibraries)
            {
                foreach (var assemblyName in referencingLibrary.Assemblies)
                {
                    yield return Assembly.Load(assemblyName);
                }
            }
        }
    }
}