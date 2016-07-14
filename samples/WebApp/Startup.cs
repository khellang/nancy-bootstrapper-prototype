namespace WebApp
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.AspNetCore;

    public class Startup : IStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services) => services.AddNancy().BuildServiceProvider();

        public void Configure(IApplicationBuilder app) => app.UseNancy();

        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseStartup<Startup>()
                .UseKestrel()
                .Build()
                .Run();
        }
    }
}

