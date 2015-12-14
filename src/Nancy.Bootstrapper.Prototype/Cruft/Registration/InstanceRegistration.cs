using System;

namespace ConsoleApplication7.Cruft.Registration
{
    public class InstanceRegistration : Registration
    {
        public InstanceRegistration(Type serviceType, object instance)
            : base(serviceType, Lifetime.Singleton)
        {
            Instance = instance;
        }

        public object Instance { get; }
    }
}