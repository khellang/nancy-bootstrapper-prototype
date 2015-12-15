using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public interface IFrameworkConfiguration
    {
        ITypeRegistrationFactory<IEngine> Engine { get; }

        ICollectionTypeRegistrationFactory<ISerializer> Serializers { get; }

        IContainerRegistry GetRegistry(ITypeCatalog typeCatalog);
    }
}