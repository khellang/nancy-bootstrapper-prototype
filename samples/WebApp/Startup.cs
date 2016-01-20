using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Nancy.Bootstrapper.Prototype.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
        }

        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>(args);
        }
    }
}
