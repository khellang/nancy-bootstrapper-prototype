namespace Nancy.Core
{
    using System.Reflection;
    using Microsoft.Extensions.DependencyModel;
    using Microsoft.Extensions.PlatformAbstractions;
    using Nancy.Core.Scanning;

    public class DefaultPlatformServices : IPlatformServices
    {
        public DefaultPlatformServices(ApplicationEnvironment environment)
        {
            Check.NotNull(environment, nameof(environment));

            var assemblyName = new AssemblyName(environment.ApplicationName);
            var assembly = Assembly.Load(assemblyName);
            var context = DependencyContext.Load(assembly);

            this.AssemblyCatalog = new DependencyContextAssemblyCatalog(assembly, context);
            this.TypeCatalog = new TypeCatalog(this.AssemblyCatalog);
            this.BootstrapperLocator = new BootstrapperLocator(this.TypeCatalog);
        }

        public IAssemblyCatalog AssemblyCatalog { get; }

        public ITypeCatalog TypeCatalog { get; }

        public IBootstrapperLocator BootstrapperLocator { get; }
    }
}
