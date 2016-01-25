using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Bootstrappers.AspNet;

namespace WebApp
{
    public class Startup
    {
        private AspNetBootstrapper Bootstrapper { get; } = new AspNetBootstrapper();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddNancy(Bootstrapper);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseNancy(Bootstrapper);
        }

        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
