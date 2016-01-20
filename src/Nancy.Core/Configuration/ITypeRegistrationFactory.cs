using System;
using Nancy.Core.Registration;

namespace Nancy.Core.Configuration
{
    public interface ITypeRegistrationFactory<in TService> : IRegistrationFactory<TypeRegistration>
    {
        void Use<TImplementation>() where TImplementation : TService;

        void Use(Type implementationType);
    }
}