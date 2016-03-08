namespace Nancy.Bootstrappers.AspNetCore
{
    using System;

    internal static class ServiceProviderExtensions
    {
        public static IServiceProvider AsDisposable(this IServiceProvider provider, bool shouldDispose)
        {
            return new DisposableServiceProvider(provider, shouldDispose);
        }

        private class DisposableServiceProvider : IServiceProvider, IDisposable
        {
            private readonly IServiceProvider provider;

            private readonly bool shouldDispose;

            public DisposableServiceProvider(IServiceProvider provider, bool shouldDispose)
            {
                this.provider = provider;
                this.shouldDispose = shouldDispose;
            }

            public object GetService(Type serviceType)
            {
                return this.provider.GetService(serviceType);
            }

            public void Dispose()
            {
                if (this.shouldDispose)
                {
                    (this.provider as IDisposable)?.Dispose();
                }
            }
        }
    }
}
