namespace Nancy.Core.Configuration
{
    using Nancy.Core.Registration;
    using Nancy.Core.Scanning;

    public interface IRegistrationFactory<out TRegistration> where TRegistration : ContainerRegistration
    {
        TRegistration GetRegistration(ITypeCatalog typeCatalog);
    }
}
