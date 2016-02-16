using System;

namespace Nancy.Bootstrappers.AspNet
{
    internal static class ServiceProviderExtensions
    {
        public static IDisposableServiceProvider AsDisposable(this IServiceProvider provider, bool shouldDispose)
        {
            return new DisposableServiceProvider(provider, shouldDispose);
        }

        private class DisposableServiceProvider : IDisposableServiceProvider
        {
            public DisposableServiceProvider(IServiceProvider provider, bool shouldDispose)
            {
                Provider = provider;
                ShouldDispose = shouldDispose;
            }

            private IServiceProvider Provider { get; }

            private bool ShouldDispose { get; }

            public object GetService(Type serviceType)
            {
                return Provider.GetService(serviceType);
            }

            public void Dispose()
            {
                if (ShouldDispose)
                {
                    (Provider as IDisposable)?.Dispose();
                }
            }
        }
    }
}