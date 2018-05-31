namespace Nancy.Core
{
    using System.Reflection;
    using Nancy.Core.Scanning;

    public class DefaultPlatform : IPlatform
    {
        private DefaultPlatform(IAssemblyCatalog assemblyCatalog)
        {
            this.AssemblyCatalog = assemblyCatalog;
            this.TypeCatalog = new TypeCatalog(this.AssemblyCatalog);
            this.BootstrapperLocator = new BootstrapperLocator(this.TypeCatalog);
        }

        static DefaultPlatform()
        {
            var assembly = Assembly.GetEntryAssembly();
            var assemblyCatalog = new DependencyContextAssemblyCatalog(assembly);
            Instance = new DefaultPlatform(assemblyCatalog);
        }

        public static IPlatform Instance { get; }

        public IAssemblyCatalog AssemblyCatalog { get; }

        public ITypeCatalog TypeCatalog { get; }

        public IBootstrapperLocator BootstrapperLocator { get; }
    }
}
