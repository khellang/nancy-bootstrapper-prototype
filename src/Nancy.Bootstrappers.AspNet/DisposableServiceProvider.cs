using System;

namespace Nancy.Bootstrappers.AspNet
{
    internal class DisposableServiceProvider : IDisposableServiceProvider
    {
        public DisposableServiceProvider(IServiceProvider provider)
        {
            Provider = provider;
        }

        private IServiceProvider Provider { get; }

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