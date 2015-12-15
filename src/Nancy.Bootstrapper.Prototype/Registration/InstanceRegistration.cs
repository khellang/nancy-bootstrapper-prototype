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

        public override string ToString()
        {
            return $"{Lifetime} - {ServiceType.Name} -> {Instance}";
        }
    }
}