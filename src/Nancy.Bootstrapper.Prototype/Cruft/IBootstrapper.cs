namespace Nancy.Bootstrapper.Prototype.Cruft
{
    public interface IBootstrapper
    {
        IApplication InitializeApplication(ITypeCatalog typeCatalog);
    }
}