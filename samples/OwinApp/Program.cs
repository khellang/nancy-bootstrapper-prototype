namespace OwinApp
{
    using System;
    using Microsoft.Owin.Hosting;
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
}
