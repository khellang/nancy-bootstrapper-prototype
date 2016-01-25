using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Core;
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
                var serviceProvider = context.RequestServices
                    ?? ScopeFactory.CreateScope().ServiceProvider;

                return new DisposableServiceProvider(serviceProvider);
            }

            protected override IEngine ComposeEngine(IServiceProvider provider)
            {
                return provider.GetRequiredService<IEngine>();
            }
        }
    }
}