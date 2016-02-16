using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Nancy.Core;
using Nancy.Core.Http;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            await LocatedBootstrapper();

            await ExistingBootstrapper();
        }

        private static async Task LocatedBootstrapper()
        {
            var platformServices = DefaultPlatformServices.Instance;

            var bootstrapper = platformServices.BootstrapperLocator.GetBootstrapper();

            using (var application = bootstrapper.InitializeApplication(platformServices))
            {
                var context = new DefaultHttpContext();

                await application.HandleRequest(context, CancellationToken.None);
            }
        }

        private static async Task ExistingBootstrapper()
        {
            var bootstrapper = new CustomBootstrapper();

            var builder = new ContainerBuilder();

            // Register stuff in the container...

            bootstrapper.Populate(builder);

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
