using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Core;
using StructureMap;

namespace WebApp
{
    public class Startup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Container.Populate(services);

            Container.Populate(Bootstrapper);

            return Container.GetInstance<IServiceProvider>();
        }

        private Container Container { get; } = new Container();

        private StructureMapBootstrapper Bootstrapper { get; } = new StructureMapBootstrapper();

        public void Configure(IApplicationBuilder app)
        {
            app.UseNancy(Bootstrapper, Container);
        }

        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
