namespace Nancy.Core.Scanning
{
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.Extensions.PlatformAbstractions;

    public class LibraryManagerAssemblyCatalog : IAssemblyCatalog
    {
        private readonly ILibraryManager libraryManager;

        public LibraryManagerAssemblyCatalog(ILibraryManager libraryManager)
        {
            Check.NotNull(libraryManager, nameof(libraryManager));

            this.libraryManager = libraryManager;
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            var nancyAssembly = typeof(IEngine).GetTypeInfo().Assembly;

            yield return nancyAssembly;

            var nancyAssemblyName = nancyAssembly.GetName().Name;

            var referencingLibraries = this.libraryManager.GetReferencingLibraries(nancyAssemblyName);

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
