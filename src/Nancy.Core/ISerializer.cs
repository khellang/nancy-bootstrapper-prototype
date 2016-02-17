namespace Nancy.Core
{
    using System;

    public interface ISerializer
    {
        string Serialize(object value);

        object Deserialize(string value, Type type);
    }
}
