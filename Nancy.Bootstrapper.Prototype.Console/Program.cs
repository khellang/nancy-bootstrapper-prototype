using System.Threading;
using Nancy.Bootstrapper.Prototype.Cruft.Http;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bootstrapper = new CustomBootstrapper();

            using (var application = bootstrapper.InitializeApplication())
            {
                var request = new HttpRequest();

                var response = application.HandleRequest(request, CancellationToken.None);
            }
        }
    }
}
