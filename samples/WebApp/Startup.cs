namespace WebApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.AspNetCore;

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
            new WebHostBuilder()
                .UseServer("Microsoft.AspNetCore.Server.Kestrel")
                .UseDefaultConfiguration(args)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}

