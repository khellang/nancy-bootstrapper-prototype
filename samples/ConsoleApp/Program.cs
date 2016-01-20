using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNet.Http.Internal;
using Nancy.Core;

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
            var bootstrapper = PlatformServices.Default.BootstrapperLocator.GetBootstrapper();

            using (var application = bootstrapper.InitializeApplication())
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
