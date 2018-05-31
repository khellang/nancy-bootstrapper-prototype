namespace AspNetApp
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.AspNetCore;

    public class Program : StartupBase
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder<Program>(args).Build().Run();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddNancy();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseNancy();
        }
    }
}

