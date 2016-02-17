namespace ConsoleApp
{
    using System;
    using Nancy.Core;

    public class CustomSerializer : ISerializer, IDisposable
    {
        public CustomSerializer()
        {
            Console.WriteLine("Created CustomSerializer.");
        }

        public void Dispose()
        {
            Console.WriteLine("Disposed CustomSerializer.");
        }

        public object Deserialize(string value, Type type)
        {
            throw new NotImplementedException();
        }

        public string Serialize(object value)
        {
            throw new NotImplementedException();
        }
    }
}
