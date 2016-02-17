namespace Nancy.Core.Configuration
{
    using System;
    using Nancy.Core.Registration;

    public interface ICollectionTypeRegistrationFactory<in TService> : IRegistrationFactory<CollectionTypeRegistration>
    {
        void Use<TImplementation>() where TImplementation : TService;

        void Use(Type implementationType);
    }
}
