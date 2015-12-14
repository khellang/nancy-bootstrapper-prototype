using Autofac;
using Nancy.Bootstrapper.Prototype.Bootstrappers.Autofac;
using Nancy.Bootstrapper.Prototype.Cruft;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class CustomBootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureApplication(IApplicationBuilder<ContainerBuilder> app)
        {
        }
    }
}