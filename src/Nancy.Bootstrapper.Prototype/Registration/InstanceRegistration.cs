using System;

namespace Nancy.Bootstrapper.Prototype.Registration
{
    public class InstanceRegistration : ContainerRegistration
    {
        public InstanceRegistration(Type serviceType, object instance)
            : base(serviceType, Lifetime.Singleton)
        {
            Instance = instance;
        }

        public object Instance { get; }

        public static InstanceRegistration Create<TService>(TService instance)
        {
            return new InstanceRegistration(typeof(TService), instance);
        }

        public override string ToString()
        {
            return $"{Lifetime} - {ServiceType.Name} -> {Instance}";
        }
    }
}