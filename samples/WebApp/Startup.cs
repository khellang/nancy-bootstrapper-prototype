using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Bootstrappers.AspNet;

namespace WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddNancy();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseNancy();
        }

        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
