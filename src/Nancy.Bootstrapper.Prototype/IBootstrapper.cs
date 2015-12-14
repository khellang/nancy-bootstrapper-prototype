namespace Nancy.Bootstrapper.Prototype
{
    public interface IBootstrapper
    {
        IApplication InitializeApplication(ITypeCatalog typeCatalog);
    }
}