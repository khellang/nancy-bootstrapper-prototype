using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication7.Cruft.Http;

namespace ConsoleApplication7
{
    public static class Program
    {
        public static void Main(string[] args) => MainAsync(args).Wait();

        private static async Task MainAsync(string[] args)
        {
            var bootstrapper = new CustomBootstrapper();

            using (var application = bootstrapper.InitializeApplication())
            {
                await application.HandleRequest(new HttpRequest(), CancellationToken.None);
            }
        }
    }
}
