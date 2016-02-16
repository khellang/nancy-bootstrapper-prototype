using System;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Core;
using Nancy.Core.Http;
using Nancy.Core.Registration;

namespace Nancy.Bootstrappers.AspNet
{
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
            return services.BuildServiceProvider().AsDisposable(shouldDispose: true);
        }

        protected sealed override void ValidateContainerConfiguration(IDisposableServiceProvider container)
        {
            // Not supported.
        }

        protected sealed override IApplication CreateApplication(IDisposableServiceProvider provider, bool shouldDispose)
        {
            return new Application(provider, shouldDispose);
        }

        private sealed class Application : Application<IDisposableServiceProvider>
        {
            public Application(IDisposableServiceProvider provider, bool shouldDispose)
                : base(provider, shouldDispose)
            {
                ScopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            }

            private IServiceScopeFactory ScopeFactory { get; }

            protected override IDisposableServiceProvider BeginRequestScope(HttpContext context, IDisposableServiceProvider provider)
            {
                IServiceProvider requestServices;
                if (TryGetRequestServices(context, out requestServices))
                {
                    // We want to reuse the existing request services instead
                    // of creating a new Nancy-specific scope if we can.

                    return requestServices.AsDisposable(shouldDispose: false);
                }

                var requestScope = ScopeFactory.CreateScope();

                var requestProvider = requestScope.ServiceProvider;

                return requestProvider.AsDisposable(shouldDispose: true);
            }

            protected override IEngine ComposeEngine(IDisposableServiceProvider provider)
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