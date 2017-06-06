namespace AspNetApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.AspNetCore;

    public static class Program
    {
        public static void Main(string[] args)
        {
            const string url = "http://localhost:5000";

            new WebHostBuilder()
                .UseStartup<Startup>()
                .UseKestrel()
                .UseUrls(url)
                .Build()
                .Run();
        }

        private class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddNancy();
            }

            public void Configure(IApplicationBuilder app)
            {
                app.UseNancy();
            }
        }
    }
}

