using System;
using System.Collections.Generic;
using Nancy.Bootstrapper.Prototype.Registration;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public interface ICollectionTypeRegistrationFactory<in TService> :
        IRegistrationFactory<CollectionTypeRegistration>,
        IEnumerable<Type>
    {
        void Use<TImplementation>() where TImplementation : TService;

        void Use(Type implementationType);
    }
}