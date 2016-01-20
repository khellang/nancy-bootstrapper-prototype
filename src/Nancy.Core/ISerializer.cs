using System;

namespace Nancy.Core
{
    public interface ISerializer
    {
        string Serialize(object value);

        object Deserialize(string value, Type type);
    }
}