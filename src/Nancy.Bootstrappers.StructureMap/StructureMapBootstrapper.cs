namespace Nancy.Bootstrappers.StructureMap
{
    using global::StructureMap;
    using global::StructureMap.Pipeline;
    using Nancy.Core;
    using Nancy.Core.Http;
    using Nancy.Core.Registration;

    public class StructureMapBootstrapper : Bootstrapper<IContainer>
    {
        protected sealed override IContainer CreateContainer()
        {
            return new Container();
        }

        protected sealed override void Register(IContainer container, IContainerRegistry registry)
        {
            container.Configure(config => config.AddRegistry(registry));
        }

        protected sealed override void ValidateContainerConfiguration(IContainer container)
        {
            container.AssertConfigurationIsValid();

            // We don't want to keep the container-scoped
            // instances around after verifying the configuration.
            foreach (var instance in container.Model.AllInstances)
            {
                if (instance.Lifecycle == Lifecycles.Container)
                {
                    instance.EjectObject();
                }
            }
        }

        protected sealed override IApplication<IContainer> CreateApplication(ConditionalDisposable<IContainer> container)
        {
            return new Application(container);
        }

        private sealed class Application : Application<IContainer>
        {
            public Application(ConditionalDisposable<IContainer> container) : base(container)
            {
            }

            protected override IContainer BeginRequestScope(HttpContext context, IContainer container)
            {
                return container.GetNestedContainer();
            }

            protected override IEngine ComposeEngine(IContainer container)
            {
                return container.GetInstance<IEngine>();
            }
        }
    }
}
