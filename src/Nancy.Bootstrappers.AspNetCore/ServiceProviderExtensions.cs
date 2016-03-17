namespace Nancy.Bootstrappers.AspNetCore
{
    using System;

    public static class ServiceProviderExtensions
    {
        public static IDisposableServiceProvider AsDisposable(this IServiceProvider provider)
        {
            return new DisposableServiceProvider(provider);
        }

        private class DisposableServiceProvider : IDisposableServiceProvider
        {
            private readonly IServiceProvider provider;

            public DisposableServiceProvider(IServiceProvider provider)
            {
                this.provider = provider;
            }

            public object GetService(Type serviceType)
            {
                return this.provider.GetService(serviceType);
            }

            public void Dispose()
            {
                (this.provider as IDisposable)?.Dispose();
            }
        }
    }
}