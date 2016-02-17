namespace ConsoleApp
{
    using System;

    public class RequestService : IRequestService
    {
        public RequestService()
        {
            Console.WriteLine("Created RequestService.");
        }

        public void Dispose()
        {
            Console.WriteLine("Disposed RequestService.");
        }
    }
}
