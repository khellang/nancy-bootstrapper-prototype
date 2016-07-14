namespace Nancy.Core.Scanning
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyModel;

    public class DependencyContextAssemblyCatalog : IAssemblyCatalog
    {
        private static readonly Assembly NancyAssembly = typeof(IEngine).GetTypeInfo().Assembly;

        private readonly Assembly entryAssembly;

        private readonly DependencyContext dependencyContext;

        public DependencyContextAssemblyCatalog(Assembly entryAssembly, DependencyContext dependencyContext)
        {
            Check.NotNull(entryAssembly, nameof(entryAssembly));

            this.entryAssembly = entryAssembly;
            this.dependencyContext = dependencyContext;
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            yield return NancyAssembly;

            if (this.dependencyContext == null)
            {
                yield return this.entryAssembly;
            }
            else
            {
                foreach (var library in this.dependencyContext.RuntimeLibraries)
                {
                    if (IsReferencingNancy(library))
                    {
                        var assemblyNames = library.GetDefaultAssemblyNames(this.dependencyContext);

                        foreach (var assemblyName in assemblyNames)
                        {
                            yield return Assembly.Load(assemblyName);
                        }
                    }
                }
            }
        }

        private static bool IsReferencingNancy(Library library)
        {
            return library.Dependencies.Any(dependency => dependency.Name.Equals(NancyAssembly.GetName().Name));
        }
    }
}
