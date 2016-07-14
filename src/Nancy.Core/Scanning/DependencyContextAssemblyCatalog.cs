namespace Nancy.Core.Scanning
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyModel;
    using Microsoft.Extensions.PlatformAbstractions;

    public class DependencyContextAssemblyCatalog : IAssemblyCatalog
    {
        private static readonly Assembly NancyAssembly = typeof(IEngine).GetTypeInfo().Assembly;

        private readonly Assembly entryAssembly;

        private readonly DependencyContext dependencyContext;

        public DependencyContextAssemblyCatalog(ApplicationEnvironment environment)
        {
            Check.NotNull(environment, nameof(environment));

            var assemblyName = new AssemblyName(environment.ApplicationName);

            this.entryAssembly = Assembly.Load(assemblyName);
            this.dependencyContext = DependencyContext.Load(this.entryAssembly);
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
