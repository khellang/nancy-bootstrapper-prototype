namespace Nancy.Core
{
    using System;
    using Newtonsoft.Json;

    public class JsonNetSerializer : ISerializer
    {
        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.None);
        }

        public object Deserialize(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type);
        }
    }
}
