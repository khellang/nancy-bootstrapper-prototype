using System.Collections.Generic;
using Autofac;
using ConsoleApplication7.Bootstrappers.Autofac;
using ConsoleApplication7.Cruft;

namespace ConsoleApplication7
{
    public class CustomBootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureApplication(IApplicationBuilder<ContainerBuilder> app)
        {
        }
    }
}