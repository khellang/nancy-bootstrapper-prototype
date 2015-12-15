using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public interface IFrameworkConfiguration
    {
        IRegistrationFactory<IEngine> Engine { get; }

        IContainerRegistry GetRegistry(ITypeCatalog typeCatalog);
    }
}