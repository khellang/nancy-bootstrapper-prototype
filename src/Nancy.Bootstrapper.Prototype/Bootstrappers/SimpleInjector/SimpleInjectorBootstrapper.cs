using ConsoleApplication7.Cruft;
using ConsoleApplication7.Cruft.Registration;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace ConsoleApplication7.Bootstrappers.SimpleInjector
{
    public class SimpleInjectorBootstrapper : Bootstrapper<Container>
    {
        protected sealed override Container CreateContainer()
        {
            return new Container
            {
                Options = { DefaultScopedLifestyle = new ExecutionContextScopeLifestyle() }
            };
        }

        protected sealed override void Register(Container builder, IContainerRegistry registry)
        {
            builder.Register(registry);
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