using System.Threading;
using Microsoft.Extensions.PlatformAbstractions;
using Nancy.Bootstrapper.Prototype.Cruft;
using Nancy.Bootstrapper.Prototype.Cruft.Http;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var libraryManager = PlatformServices.Default.LibraryManager;

            var assemblyCatalog = new LibraryManagerAssemblyCatalog(libraryManager);

            var typeCatalog = new DefaultTypeCatalog(assemblyCatalog);

            var bootstrapperLocator = new BootstrapperLocator(typeCatalog);

            var bootstrapper = bootstrapperLocator.GetBootstrapper();

            using (var application = bootstrapper.InitializeApplication(typeCatalog))
            {
                var request = new HttpRequest();

                var response = application.HandleRequest(request, CancellationToken.None);
            }
        }
    }
}
