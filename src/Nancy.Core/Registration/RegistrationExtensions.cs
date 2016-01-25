namespace Nancy.Core.Registration
{
    internal static class RegistrationExtensions
    {
        public static InstanceRegistration AsInstanceRegistration<TService>(this TService instance)
        {
            return new InstanceRegistration(typeof(TService), instance);
        }
    }
}