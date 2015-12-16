using System;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Bootstrapper.Prototype.Registration;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.AspNet
{
    public class AspNetBootstrapper : Bootstrapper<IServiceCollection, IServiceScope>
    {
        protected sealed override IServiceCollection CreateBuilder()
        {
            return new ServiceCollection();
        }

        protected sealed override void Register(IServiceCollection builder, IContainerRegistry registry)
        {
            builder.AddRegistry(registry);
        }

        protected sealed override IServiceScope BuildContainer(IServiceCollection builder)
        {
            return CreateScope(builder.BuildServiceProvider());
        }

        protected sealed override IApplication CreateApplication(IServiceScope container)
        {
            return new AspNetApplication(container);
        }

        private static IServiceScope CreateScope(IServiceProvider container)
        {
            return container.GetRequiredService<IServiceScopeFactory>().CreateScope();
        }

        private sealed class AspNetApplication : Application<IServiceScope>
        {
            public AspNetApplication(IServiceScope container) : base(container)
            {
            }

            protected override IServiceScope BeginRequestScope(IServiceScope container)
            {
                return CreateScope(container.ServiceProvider);
            }

            protected override IEngine ComposeEngine(IServiceScope container)
            {
                return container.ServiceProvider.GetRequiredService<IEngine>();
            }
        }
    }
}