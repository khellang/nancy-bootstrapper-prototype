using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public interface IRegistrationFactory<out TRegistration> where TRegistration : ContainerRegistration
    {
        TRegistration GetRegistration(ITypeCatalog typeCatalog);
    }
}