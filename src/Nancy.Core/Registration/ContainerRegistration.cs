namespace Nancy.Core.Registration
{
    using System;

    public abstract class ContainerRegistration
    {
        protected ContainerRegistration(Type serviceType, Lifetime lifetime)
        {
            this.ServiceType = serviceType;
            this.Lifetime = lifetime;
        }

        public Type ServiceType { get; }

        public Lifetime Lifetime { get; }
    }
}
