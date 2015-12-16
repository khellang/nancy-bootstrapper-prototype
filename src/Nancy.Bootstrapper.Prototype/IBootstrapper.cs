using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype
{
    public interface IBootstrapper
    {
        IApplication InitializeApplication(ITypeCatalog typeCatalog);
    }

    public interface IBootstrapper<in TBuilder, in TContainer> : IBootstrapper
    {
        void Populate(TBuilder builder, ITypeCatalog typeCatalog);

        IApplication InitializeApplication(TContainer container);
    }
}