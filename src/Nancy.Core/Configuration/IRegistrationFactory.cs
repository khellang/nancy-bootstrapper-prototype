using Nancy.Core.Registration;
using Nancy.Core.Scanning;

namespace Nancy.Core.Configuration
{
    public interface IRegistrationFactory<out TRegistration> where TRegistration : ContainerRegistration
    {
        TRegistration GetRegistration(ITypeCatalog typeCatalog);
    }
}