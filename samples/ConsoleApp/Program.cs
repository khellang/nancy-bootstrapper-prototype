namespace ConsoleApp
{
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac;
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
            var platform = DefaultPlatform.Instance;

            await LocatedBootstrapper(platform);

            await ExistingBootstrapper(platform);
        }

        private static async Task LocatedBootstrapper(IPlatform platform)
        {
            var bootstrapper = platform.BootstrapperLocator.GetBootstrapper();

            using (var application = bootstrapper.InitializeApplication(platform))
            {
                var context = new DefaultHttpContext();

                await application.HandleRequest(context, CancellationToken.None);
            }
        }

        private static async Task ExistingBootstrapper(IPlatform platform)
        {
            var bootstrapper = new CustomBootstrapper();

            var builder = new ContainerBuilder();

            // Register stuff in the container...

            bootstrapper.Populate(builder, platform);

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
