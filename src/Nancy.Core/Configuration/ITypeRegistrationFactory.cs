using System;
using Nancy.Bootstrapper.Prototype.Registration;

namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public interface ITypeRegistrationFactory<in TService> : IRegistrationFactory<TypeRegistration>
    {
        void Use<TImplementation>() where TImplementation : TService;

        void Use(Type implementationType);
    }
}