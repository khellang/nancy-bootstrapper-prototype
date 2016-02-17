namespace ConsoleApp
{
    using Autofac;
    using Nancy.Bootstrappers.Autofac;
    using Nancy.Core.Configuration;

    public class CustomBootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureApplication(IApplicationConfiguration<ContainerBuilder> app)
        {
            app.Container.RegisterType<RequestService>().As<IRequestService>().InstancePerRequest();

            app.Framework.Engine.Use<CustomEngine>();

            app.Framework.Serializers.Use<CustomSerializer>();
        }
    }
}
