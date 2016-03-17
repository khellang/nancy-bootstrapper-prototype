namespace Nancy.Bootstrappers.AspNetCore
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.Core;
    using Nancy.Core.Http;
    using Nancy.Core.Registration;

    public class AspNetBootstrapper : Bootstrapper<IServiceCollection, IServiceProvider>
    {
        protected sealed override IServiceCollection CreateBuilder()
        {
            return new ServiceCollection();
        }

        protected sealed override void Register(IServiceCollection services, IContainerRegistry registry)
        {
            services.AddRegistry(registry);
        }

        protected sealed override IServiceProvider BuildContainer(IServiceCollection services)
        {
            // Make sure we wrap the instance we create in a disposable.
            return services.BuildServiceProvider();
        }

        protected sealed override void ValidateContainerConfiguration(IServiceProvider container)
        {
            // Not supported.
        }

        protected sealed override IApplication<IServiceProvider> CreateApplication(IServiceProvider provider, bool shouldDispose)
        {
            return new Application(provider, shouldDispose);
        }

        private sealed class Application : Application<IServiceProvider, IServiceScope>
        {
            private readonly IServiceScopeFactory scopeFactory;

            public Application(IServiceProvider provider, bool shouldDispose)
                : base(provider, shouldDispose)
            {
                this.scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            }

            protected override bool TryGetExistingScope(HttpContext context, out IServiceScope provider)
            {
                object value;
                // If we're running in ASP.NET, this should be set by the Nancy middleware.
                if (context.Items.TryGetValue(Constants.AspNetRequestServices, out value))
                {
                    var services = value as IServiceProvider;

                    if (services != null)
                    {
                        provider = new ExistingServiceScope(services);
                        return true;
                    }
                }

                provider = null;
                return false;
            }

            protected override IServiceScope BeginRequestScope(HttpContext context, IServiceProvider provider)
            {
                return this.scopeFactory.CreateScope();
            }

            protected override IEngine ComposeEngine(IServiceProvider container, IServiceScope scope)
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
