using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype
{
    public interface IBootstrapper
    {
        IApplication InitializeApplication(IAssemblyCatalog assemblyCatalog, ITypeCatalog typeCatalog);
    }
}