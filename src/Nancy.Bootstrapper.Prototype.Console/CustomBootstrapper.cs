using Autofac;
using Nancy.Bootstrapper.Prototype.Bootstrappers.Autofac;
using Nancy.Bootstrapper.Prototype.Configuration;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class CustomBootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureApplication(IApplicationConfiguration<ContainerBuilder> app)
        {
            app.Container.RegisterType<RequestService>()
                .As<IRequestService>()
                .InstancePerRequest();

            app.Framework.Engine.Use<CustomEngine>();

            app.Framework.Serializers.Use<CustomSerializer>();
        }
    }
}