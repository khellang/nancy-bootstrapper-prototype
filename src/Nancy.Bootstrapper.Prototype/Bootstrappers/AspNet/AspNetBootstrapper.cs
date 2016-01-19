using Microsoft.Extensions.DependencyInjection;
using Nancy.Bootstrapper.Prototype.Registration;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.AspNet
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
            return new AspNetApplication(provider);
        }

        private sealed class AspNetApplication : Application<IServiceProvider, IServiceScope>
        {
            public AspNetApplication(IServiceProvider provider) : base(provider)
            {
                ScopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            }

            private IServiceScopeFactory ScopeFactory { get; }

            protected override IServiceScope BeginRequestScope(IServiceProvider provider)
            {
                return ScopeFactory.CreateScope();
            }

            protected override IEngine ComposeEngine(IServiceProvider provider, IServiceScope scope)
            {
                return scope.ServiceProvider.GetRequiredService<IEngine>();
            }
        }
    }
}