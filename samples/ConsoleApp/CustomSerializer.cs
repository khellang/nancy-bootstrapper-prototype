using System;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class CustomSerializer : ISerializer, IDisposable
    {
        public CustomSerializer()
        {
            System.Console.WriteLine("Created CustomSerializer.");
        }

        public object Deserialize(string value, Type type)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            System.Console.WriteLine("Disposed CustomSerializer.");
        }

        public string Serialize(object value)
        {
            throw new NotImplementedException();
        }
    }
}