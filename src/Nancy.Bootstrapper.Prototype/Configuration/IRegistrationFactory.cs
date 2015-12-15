using System;
using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public interface IRegistrationFactory
    {
        TypeRegistration GetRegistration(ITypeCatalog typeCatalog);

        void Use(Type implementationType);
    }

    public interface IRegistrationFactory<in TService> : IRegistrationFactory
    {
        void Use<TImplementation>() where TImplementation : TService;
    }
}