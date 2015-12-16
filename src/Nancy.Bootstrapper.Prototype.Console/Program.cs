using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Extensions.PlatformAbstractions;
using Nancy.Bootstrapper.Prototype.Scanning;

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

            var bootstrapperLocator = new BootstrapperLocator(typeCatalog);

            var bootstrapper = bootstrapperLocator.GetBootstrapper();

            using (var application = bootstrapper.InitializeApplication(assemblyCatalog, typeCatalog))
            {
                var context = new DefaultHttpContext();

                await application.HandleRequest(context, CancellationToken.None);
            }
        }
    }
}
