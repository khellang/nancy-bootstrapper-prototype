using Nancy.Bootstrapper.Prototype.Bootstrappers.StructureMap;
using Nancy.Bootstrapper.Prototype.Configuration;
using StructureMap.Pipeline;
using StructureMap;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class CustomBootstrapper : StructureMapBootstrapper
    {
        protected override void ConfigureApplication(IApplicationConfiguration<IContainer> app)
        {
            app.Container.Configure(c =>
                c.For<IRequestService>()
                    .Use<RequestService>()
                    .LifecycleIs<ContainerLifecycle>());

            app.Framework.Engine.Use<CustomEngine>();

            app.Framework.Serializers.Use<CustomSerializer>();
        }
    }
}