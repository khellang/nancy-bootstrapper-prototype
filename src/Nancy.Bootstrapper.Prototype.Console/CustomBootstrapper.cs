using Autofac;
using Nancy.Bootstrapper.Prototype.Bootstrappers.Autofac;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class CustomBootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureApplication(IApplicationBuilder<ContainerBuilder> app)
        {
            app.Container.RegisterType<RequestService>()
                .As<IRequestService>()
                .InstancePerRequest();

            var testTypes = app.TypeCatalog
                .GetTypesAssignableTo<IBootstrapperLocator>();
        }
    }

    public interface IRequestService
    {
    }

    public class RequestService : IRequestService
    {
    }
}