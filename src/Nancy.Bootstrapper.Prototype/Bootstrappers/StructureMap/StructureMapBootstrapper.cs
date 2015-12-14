using Nancy.Bootstrapper.Prototype.Cruft;
using Nancy.Bootstrapper.Prototype.Cruft.Registration;
using StructureMap;

namespace Nancy.Bootstrapper.Prototype.Bootstrappers.StructureMap
{
    public class StructureMapBootstrapper : Bootstrapper<IContainer>
    {
        protected sealed override IContainer CreateContainer()
        {
            return new Container();
        }

        protected sealed override void Register(IContainer builder, IContainerRegistry registry)
        {
            builder.Configure(config => config.AddRegistry(registry));
        }

        protected sealed override void ValidateContainerConfiguration(IContainer container)
        {
            container.AssertConfigurationIsValid();
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