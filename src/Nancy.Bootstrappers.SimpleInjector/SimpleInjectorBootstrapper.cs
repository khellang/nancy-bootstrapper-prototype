namespace Nancy.Bootstrappers.SimpleInjector
{
    using global::SimpleInjector;
    using global::SimpleInjector.Lifestyles;
    using Nancy.Core;
    using Nancy.Core.Http;
    using Nancy.Core.Registration;

    public class SimpleInjectorBootstrapper : Bootstrapper<Container>
    {
        private static readonly AsyncScopedLifestyle DefaultLifestyle = new AsyncScopedLifestyle();

        protected sealed override Container CreateContainer()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = DefaultLifestyle;

            return container;
        }

        protected sealed override void Register(Container container, IContainerRegistry registry)
        {
            container.Register(registry);
        }

        protected sealed override void ValidateContainerConfiguration(Container container)
        {
            container.Verify(VerificationOption.VerifyOnly);
        }

        protected sealed override IApplication<Container> CreateApplication(Disposable<Container> container)
        {
            return new Application(container);
        }

        private sealed class Application : Application<Container, Scope>
        {
            public Application(Disposable<Container> container) : base(container)
            {
            }

            protected override Scope BeginRequestScope(HttpContext context, Container container)
            {
                return AsyncScopedLifestyle.BeginScope(container);
            }

            protected override IEngine ComposeEngine(Container container, Scope scope)
            {
                return container.GetInstance<IEngine>();
            }
        }
    }
}
