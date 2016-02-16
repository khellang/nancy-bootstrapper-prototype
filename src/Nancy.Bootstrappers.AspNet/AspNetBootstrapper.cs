using Microsoft.Extensions.DependencyInjection;
using Nancy.Core;
using Nancy.Core.Http;
using Nancy.Core.Registration;

namespace Nancy.Bootstrappers.AspNet
{
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
            return new DisposableServiceProvider(services.BuildServiceProvider());
        }

        protected sealed override void ValidateContainerConfiguration(IServiceProvider container)
        {
            // Not supported.
        }

        protected sealed override IApplication CreateApplication(IServiceProvider provider)
        {
            return new Application(provider);
        }

        private sealed class Application : Application<IServiceProvider>
        {
            public Application(IServiceProvider provider) : base(provider)
            {
                ScopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            }

            private IServiceScopeFactory ScopeFactory { get; }

            protected override IServiceProvider BeginRequestScope(HttpContext context, IServiceProvider provider)
            {
                IServiceProvider requestServices;
                if (TryGetRequestServices(context, out requestServices))
                {
                    // We want to reuse the existing request services instead
                    // of creating a new Nancy-specific scope if we can.
                    return new DisposableServiceProvider(requestServices);
                }

                var requestScope = ScopeFactory.CreateScope();

                var serviceProvider = requestScope.ServiceProvider;

                return new DisposableServiceProvider(serviceProvider);
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