namespace ConsoleApp
{
    public class RequestService : IRequestService
    {
        public RequestService()
        {
            System.Console.WriteLine("Created RequestService.");
        }

        public void Dispose()
        {
            System.Console.WriteLine("Disposed RequestService.");
        }
    }
}