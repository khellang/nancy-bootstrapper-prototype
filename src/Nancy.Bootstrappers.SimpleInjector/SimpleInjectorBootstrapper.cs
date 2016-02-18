namespace Nancy.Bootstrappers.SimpleInjector
{
    using global::SimpleInjector;
    using global::SimpleInjector.Extensions.ExecutionContextScoping;
    using Nancy.Core;
    using Nancy.Core.Http;
    using Nancy.Core.Registration;

    public class SimpleInjectorBootstrapper : Bootstrapper<Container>
    {
        private static readonly ExecutionContextScopeLifestyle DefaultLifestyle = new ExecutionContextScopeLifestyle();

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

        protected sealed override IApplication<Container> CreateApplication(Container container, bool shouldDispose)
        {
            return new Application(container, shouldDispose);
        }

        private sealed class Application : Application<Container, Scope>
        {
            public Application(Container container, bool shouldDispose)
                : base(container, shouldDispose)
            {
            }

            protected override Scope BeginRequestScope(HttpContext context, Container container)
            {
                return container.BeginExecutionContextScope();
            }

            protected override IEngine ComposeEngine(Container container, Scope scope)
            {
                return container.GetInstance<IEngine>();
            }
        }
    }
}
