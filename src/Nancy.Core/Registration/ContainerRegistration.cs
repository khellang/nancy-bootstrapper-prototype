using System;

namespace Nancy.Core.Registration
{
    public abstract class ContainerRegistration
    {
        protected ContainerRegistration(Type serviceType, Lifetime lifetime)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public Type ServiceType { get; }

        public Lifetime Lifetime { get; }
    }
}