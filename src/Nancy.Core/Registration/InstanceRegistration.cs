namespace Nancy.Core.Registration
{
    using System;

    public class InstanceRegistration : ContainerRegistration
    {
        public InstanceRegistration(Type serviceType, object instance)
            : base(serviceType, Lifetime.Singleton)
        {
            this.Instance = instance;
        }

        public object Instance { get; }
    }
}
