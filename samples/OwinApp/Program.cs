namespace OwinApp
{
    using System;
    using System.Text;
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
            public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
            {
                var hellWorldBytes = Encoding.UTF8.GetBytes("Hello World!");

                context.Response.StatusCode = HttpStatusCode.Ok;

                // TODO: Set Content-Type and Content-Length.

                return context.Response.Body.WriteAsync(hellWorldBytes, 0, hellWorldBytes.Length, cancellationToken);
            }
        }
    }
}
