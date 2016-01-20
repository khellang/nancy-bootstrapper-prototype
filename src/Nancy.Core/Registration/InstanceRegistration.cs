using System;

namespace Nancy.Core.Registration
{
    public class InstanceRegistration : ContainerRegistration
    {
        public InstanceRegistration(Type serviceType, object instance)
            : base(serviceType, Lifetime.Singleton)
        {
            Instance = instance;
        }

        public object Instance { get; }
    }
}