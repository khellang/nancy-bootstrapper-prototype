using System.Threading;
using Microsoft.Extensions.PlatformAbstractions;
using Nancy.Bootstrapper.Prototype.Http;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var libraryManager = PlatformServices.Default.LibraryManager;

            var assemblyCatalog = new LibraryManagerAssemblyCatalog(libraryManager);

            var typeCatalog = new TypeCatalog(assemblyCatalog);

            var bootstrapperLocator = new BootstrapperLocator(typeCatalog);

            var bootstrapper = bootstrapperLocator.GetBootstrapper();

            using (var application = bootstrapper.InitializeApplication(assemblyCatalog, typeCatalog))
            {
                var request = new HttpRequest();

                var response = application.HandleRequest(request, CancellationToken.None);
            }
        }
    }
}
