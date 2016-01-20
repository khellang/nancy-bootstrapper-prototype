using System;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.AspNet
{
    internal class DisposableServiceProvider : IServiceProvider
    {
        public DisposableServiceProvider(System.IServiceProvider provider)
        {
            Provider = provider;
        }

        private System.IServiceProvider Provider { get; }

        public object GetService(Type serviceType)
        {
            return Provider.GetService(serviceType);
        }

        public void Dispose()
        {
            (Provider as IDisposable)?.Dispose();
        }
    }
}