﻿namespace OwinApp
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac;
    using Microsoft.Owin.Hosting;
    using Nancy.Bootstrappers.Autofac;
    using Nancy.Core;
    using Nancy.Core.Configuration;
    using Nancy.Core.Http;
    using Owin;
    using Nancy.Owin;

    public class Program
    {
        public static void Main(string[] args)
        {
            const string url = "http://localhost:8080";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Listening at {url}...");
                Console.ReadLine();
            }
        }

        private class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                app.UseNancy();
            }
        }
    }

    public class CustomBootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureApplication(IApplicationConfiguration<ContainerBuilder> app)
        {
            app.Framework.Engine.Use<CustomEngine>();
        }

        private class CustomEngine : IEngine
        {
            public async Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
            {
                context.Response.StatusCode = 200;
                context.Response.StatusCode = HttpStatusCode.Ok;

                context.Response.ContentType = "application/vnd.github.v3.raw+json; charset=utf-8";
                context.Response.ContentType = MediaRange.ApplicationJson;

                using (var writer = new StreamWriter(context.Response.Body))
                {
                    await writer.WriteLineAsync($@"{{ ""url"": ""{context.Request.Url}"" }}");
                    await writer.FlushAsync();
                }
            }
        }
    }
}