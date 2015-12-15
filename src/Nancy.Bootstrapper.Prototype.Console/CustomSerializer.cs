using System;

namespace Nancy.Bootstrapper.Prototype.Console
{
    public class CustomSerializer : ISerializer
    {
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