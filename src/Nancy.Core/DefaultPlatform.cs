namespace Nancy.Core
{
    using Microsoft.Extensions.PlatformAbstractions;
    using Nancy.Core.Scanning;

    public class DefaultPlatform : IPlatform
    {
        public DefaultPlatform(ApplicationEnvironment environment)
        {
            Check.NotNull(environment, nameof(environment));

            this.Environment = environment;
            this.AssemblyCatalog = new DependencyContextAssemblyCatalog(environment);
            this.TypeCatalog = new TypeCatalog(this.AssemblyCatalog);
            this.BootstrapperLocator = new BootstrapperLocator(this.TypeCatalog);
        }

        public ApplicationEnvironment Environment { get; }

        public IAssemblyCatalog AssemblyCatalog { get; }

        public ITypeCatalog TypeCatalog { get; }

        public IBootstrapperLocator BootstrapperLocator { get; }
    }
}
