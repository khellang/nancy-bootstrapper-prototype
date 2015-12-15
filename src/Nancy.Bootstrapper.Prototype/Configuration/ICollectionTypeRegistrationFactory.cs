using System;
using Nancy.Bootstrapper.Prototype.Registration;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public interface ICollectionTypeRegistrationFactory<in TService> : IRegistrationFactory<CollectionTypeRegistration>
    {
        void Use<TImplementation>() where TImplementation : TService;

        void Use(Type implementationType);

        void Clear();
    }
}