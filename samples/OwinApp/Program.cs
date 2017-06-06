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
            const string url = "http://localhost:5000";

            using (WebApp.Start(url, Configuration))
            {
                Console.WriteLine($"Listening at {url}...");
                Console.ReadLine();
            }
        }

        private static void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
