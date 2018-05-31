namespace Nancy.Bootstrappers.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.Core;
    using Nancy.Core.Http;
    using Nancy.Core.Registration;

    public class AspNetBootstrapper : Bootstrapper<IServiceCollection, IDisposableServiceProvider>
    {
        protected sealed override IServiceCollection CreateBuilder()
        {
            return new ServiceCollection();
        }

        protected sealed override void Register(IServiceCollection services, IContainerRegistry registry)
        {
            services.AddRegistry(registry);
        }

        protected sealed override IDisposableServiceProvider BuildContainer(IServiceCollection services)
        {
            return services.BuildServiceProvider().AsDisposable();
        }

        protected sealed override void ValidateContainerConfiguration(IDisposableServiceProvider container)
        {
            // Not supported.
        }

        protected sealed override IApplication<IDisposableServiceProvider> CreateApplication(Disposable<IDisposableServiceProvider> provider)
        {
            return new Application(provider);
        }

        private sealed class Application : Application<IDisposableServiceProvider, IServiceScope>
        {
            private readonly IServiceScopeFactory scopeFactory;

            public Application(Disposable<IDisposableServiceProvider> provider) : base(provider)
            {
                this.scopeFactory = provider.Instance.GetRequiredService<IServiceScopeFactory>();
            }

            protected override bool TryGetExistingScope(HttpContext context, out IServiceScope scope)
            {
                // If we're running in ASP.NET, this should be set by the Nancy middleware.
                if (context.Items.TryGetValue(Constants.AspNetRequestServices, out var value))
                {
                    if (value is IServiceProvider services)
                    {
                        scope = new ExistingServiceScope(services);
                        return true;
                    }
                }

                scope = null;
                return false;
            }

            protected override IServiceScope BeginRequestScope(HttpContext context, IDisposableServiceProvider provider)
            {
                return this.scopeFactory.CreateScope();
            }

            protected override IEngine ComposeEngine(IDisposableServiceProvider container, IServiceScope scope)
            {
                return scope.ServiceProvider.GetRequiredService<IEngine>();
            }

            private class ExistingServiceScope : IServiceScope
            {
                public ExistingServiceScope(IServiceProvider provider)
                {
                    this.ServiceProvider = provider;
                }

                public IServiceProvider ServiceProvider { get; }

                public void Dispose()
                {
                    // We don't want to dispose the request
                    // scope created by the ASP.NET host.
                }
            }
        }
    }
}
