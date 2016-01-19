using Nancy.Bootstrapper.Prototype.Registration;
using StructureMap;
using StructureMap.Pipeline;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.StructureMap
{
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

        protected sealed override IApplication CreateApplication(IContainer container)
        {
            return new StructureMapApplication(container);
        }

        private sealed class StructureMapApplication : Application<IContainer>
        {
            public StructureMapApplication(IContainer container) : base(container)
            {
            }

            protected override IContainer BeginRequestScope(IContainer container)
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