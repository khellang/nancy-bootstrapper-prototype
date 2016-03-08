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
            return services.BuildServiceProvider().AsDisposable(shouldDispose: true);
        }

        protected sealed override void ValidateContainerConfiguration(IServiceProvider container)
        {
            // Not supported.
        }

        protected sealed override IApplication<IServiceProvider> CreateApplication(IServiceProvider provider, bool shouldDispose)
        {
            return new Application(provider, shouldDispose);
        }

        private sealed class Application : Application<IServiceProvider>
        {
            private readonly IServiceScopeFactory scopeFactory;

            public Application(IServiceProvider provider, bool shouldDispose)
                : base(provider, shouldDispose)
            {
                this.scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            }

            protected override IServiceProvider BeginRequestScope(HttpContext context, IServiceProvider provider)
            {
                IServiceProvider requestServices;
                if (TryGetRequestServices(context, out requestServices))
                {
                    // We want to reuse the existing request services instead
                    // of creating a new Nancy-specific scope if we can.
                    return requestServices.AsDisposable(shouldDispose: false);
                }

                var requestScope = this.scopeFactory.CreateScope();

                var requestProvider = requestScope.ServiceProvider;

                return requestProvider.AsDisposable(shouldDispose: true);
            }

            protected override IEngine ComposeEngine(IServiceProvider provider)
            {
                return provider.GetRequiredService<IEngine>();
            }

            private static bool TryGetRequestServices(HttpContext context, out IServiceProvider requestServices)
            {
                object value;
                // If we're running in ASP.NET, this should be set by the Nancy middleware.
                if (context.Items.TryGetValue(Constants.AspNetRequestServices, out value))
                {
                    var services = value as IServiceProvider;
                    if (services != null)
                    {
                        requestServices = services;
                        return true;
                    }
                }

                requestServices = null;
                return false;
            }
        }
    }
}
