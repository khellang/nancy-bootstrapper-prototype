namespace Nancy.Core.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.PlatformAbstractions;

    public class LibraryManagerAssemblyCatalog : IAssemblyCatalog
    {
        private readonly Lazy<IReadOnlyCollection<Assembly>> lazyAssemblies;

        private readonly ILibraryManager libraryManager;

        public LibraryManagerAssemblyCatalog(ILibraryManager libraryManager)
        {
            this.libraryManager = libraryManager;
            this.lazyAssemblies = new Lazy<IReadOnlyCollection<Assembly>>(this.GetAssemblies);
        }

        IReadOnlyCollection<Assembly> IAssemblyCatalog.GetAssemblies()
        {
            return this.lazyAssemblies.Value;
        }

        private IReadOnlyCollection<Assembly> GetAssemblies()
        {
            var nancyAssembly = typeof(IEngine).GetTypeInfo().Assembly;

            var assemblies = new HashSet<Assembly> { nancyAssembly };

            var nancyAssemblyName = nancyAssembly.GetName().Name;

            var referencingLibraries = this.libraryManager.GetReferencingLibraries(nancyAssemblyName);

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
