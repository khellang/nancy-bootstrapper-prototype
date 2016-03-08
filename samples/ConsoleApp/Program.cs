namespace ConsoleApp
{
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac;
    using Microsoft.Extensions.PlatformAbstractions;
    using Nancy.Core;
    using Nancy.Core.Http;

    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var environment = PlatformServices.Default.Application;

            var platformServices = new DefaultPlatformServices(environment);

            await LocatedBootstrapper(platformServices);

            await ExistingBootstrapper(platformServices);
        }

        private static async Task LocatedBootstrapper(IPlatformServices platformServices)
        {
            var bootstrapper = platformServices.BootstrapperLocator.GetBootstrapper();

            using (var application = bootstrapper.InitializeApplication(platformServices))
            {
                var context = new DefaultHttpContext();

                await application.HandleRequest(context, CancellationToken.None);
            }
        }

        private static async Task ExistingBootstrapper(IPlatformServices platformServices)
        {
            var bootstrapper = new CustomBootstrapper();

            var builder = new ContainerBuilder();

            // Register stuff in the container...

            bootstrapper.Populate(builder, platformServices);

            var container = builder.Build();

            using (var application = bootstrapper.InitializeApplication(container))
            {
                //while (true)
                {
                    var context = new DefaultHttpContext();

                    await application.HandleRequest(context, CancellationToken.None);
                }
            }
        }
    }
}
