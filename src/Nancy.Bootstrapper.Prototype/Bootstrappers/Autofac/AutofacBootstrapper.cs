using Autofac;
using Autofac.Core.Lifetime;
using Nancy.Bootstrapper.Prototype.Cruft;
using Nancy.Bootstrapper.Prototype.Cruft.Registration;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.Autofac
{
    public class AutofacBootstrapper : Bootstrapper<ContainerBuilder, ILifetimeScope>
    {
        protected sealed override ContainerBuilder CreateBuilder()
        {
            return new ContainerBuilder();
        }

        protected sealed override void Register(ContainerBuilder builder, IContainerRegistry registry)
        {
            builder.Register(registry);
        }

        protected sealed override ILifetimeScope BuildContainer(ContainerBuilder builder)
        {
            return builder.Build();
        }

        protected sealed override IApplication CreateApplication(ILifetimeScope container)
        {
            return new AutofacApplication(container);
        }

        private sealed class AutofacApplication : Application<ILifetimeScope>
        {
            public AutofacApplication(ILifetimeScope container) : base(container)
            {
            }

            protected override ILifetimeScope BeginRequestScope(ILifetimeScope container)
            {
                return container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }

            protected override IEngine ComposeEngine(ILifetimeScope scope)
            {
                return scope.Resolve<IEngine>();
            }
        }
    }
}