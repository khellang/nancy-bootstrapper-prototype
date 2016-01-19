using Nancy.Bootstrapper.Prototype.Registration;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.SimpleInjector
{
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

        protected sealed override IApplication CreateApplication(Container container)
        {
            return new SimpleInjectorApplication(container);
        }

        private sealed class SimpleInjectorApplication : Application<Container, Scope>
        {
            public SimpleInjectorApplication(Container container) : base(container)
            {
            }

            protected override Scope BeginRequestScope(Container container)
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