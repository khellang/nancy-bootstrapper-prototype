using Nancy.Core.Scanning;

namespace Nancy.Core
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