using System;

namespace Nancy.Bootstrapper.Prototype.Cruft.Registration
{
    public abstract class Registration
    {
        protected Registration(Type serviceType, Lifetime lifetime)
        {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public Type ServiceType { get; }

        public Lifetime Lifetime { get; }
    }
}