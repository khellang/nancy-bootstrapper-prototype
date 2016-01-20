using System;
using Nancy.Core.Registration;

namespace Nancy.Core.Configuration
{
    public interface ICollectionTypeRegistrationFactory<in TService> : IRegistrationFactory<CollectionTypeRegistration>
    {
        void Use<TImplementation>() where TImplementation : TService;

        void Use(Type implementationType);
    }
}