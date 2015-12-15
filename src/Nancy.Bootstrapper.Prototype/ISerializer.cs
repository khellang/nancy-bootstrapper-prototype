using System;

namespace Nancy.Bootstrapper.Prototype
{
    public interface ISerializer
    {
        string Serialize(object value);

        object Deserialize(string value, Type type);
    }
}