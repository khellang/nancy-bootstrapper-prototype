using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Extensions.PlatformAbstractions;
using Nancy.Bootstrapper.Prototype.Scanning;
using Nancy.Bootstrapper.Prototype.Bootstrappers.Autofac;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var libraryManager = PlatformServices.Default.LibraryManager;

            var assemblyCatalog = new LibraryManagerAssemblyCatalog(libraryManager);

            var typeCatalog = new TypeCatalog(assemblyCatalog);

            await LocatedBootstrapper(typeCatalog);

            await ExistingBootstrapper(typeCatalog);
        }

        private static async Task LocatedBootstrapper(TypeCatalog typeCatalog)
        {
            var bootstrapperLocator = new BootstrapperLocator(typeCatalog);

            var bootstrapper = bootstrapperLocator.GetBootstrapper();

            using (var application = bootstrapper.InitializeApplication(typeCatalog))
            {
                var context = new DefaultHttpContext();

                await application.HandleRequest(context, CancellationToken.None);
            }
        }

        private static async Task ExistingBootstrapper(TypeCatalog typeCatalog)
        {
            var bpotstrapper = new CustomBootstrapper();

            var builder = new ContainerBuilder();

            // Register stuff in the container...

            bpotstrapper.Populate(builder, typeCatalog);

            var container = builder.Build();

            using (var application = bpotstrapper.InitializeApplication(container))
            {
                var context = new DefaultHttpContext();

                await application.HandleRequest(context, CancellationToken.None);
            }
        }
    }
}
