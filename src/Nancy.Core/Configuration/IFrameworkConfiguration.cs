namespace Nancy.Core.Configuration
{
    public interface IFrameworkConfiguration
    {
        ITypeRegistrationFactory<IEngine> Engine { get; }

        ICollectionTypeRegistrationFactory<ISerializer> Serializers { get; }
    }
}