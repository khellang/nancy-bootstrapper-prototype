namespace Nancy.Bootstrappers.Autofac
{
    using global::Autofac;
    using global::Autofac.Core.Lifetime;
    using Nancy.Core;
    using Nancy.Core.Http;
    using Nancy.Core.Registration;

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

        protected sealed override void ValidateContainerConfiguration(ILifetimeScope container)
        {
            // Not supported.
        }

        protected sealed override IApplication CreateApplication(ILifetimeScope lifetimeScope, bool shouldDispose)
        {
            return new Application(lifetimeScope, shouldDispose);
        }

        private sealed class Application : Application<ILifetimeScope>
        {
            public Application(ILifetimeScope lifetimeScope, bool shouldDispose)
                : base(lifetimeScope, shouldDispose)
            {
            }

            protected override ILifetimeScope BeginRequestScope(HttpContext context, ILifetimeScope lifetimeScope)
            {
                return lifetimeScope.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }

            protected override IEngine ComposeEngine(ILifetimeScope lifetimeScope)
            {
                return lifetimeScope.Resolve<IEngine>();
            }
        }
    }
}
