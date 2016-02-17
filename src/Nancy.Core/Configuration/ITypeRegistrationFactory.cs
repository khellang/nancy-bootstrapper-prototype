namespace Nancy.Core.Configuration
{
    using System;
    using Nancy.Core.Registration;

    public interface ITypeRegistrationFactory<in TService> : IRegistrationFactory<TypeRegistration>
    {
        void Use<TImplementation>() where TImplementation : TService;

        void Use(Type implementationType);
    }
}
