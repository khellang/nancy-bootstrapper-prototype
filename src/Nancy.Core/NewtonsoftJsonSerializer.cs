namespace Nancy.Core
{
    using System;
    using Newtonsoft.Json;

    public class NewtonsoftJsonSerializer : ISerializer
    {
        public string Serialize(object value)
        {
            Check.NotNull(value, nameof(value));

            return JsonConvert.SerializeObject(value, Formatting.None);
        }

        public object Deserialize(string value, Type type)
        {
            Check.NotNull(value, nameof(value));

            return JsonConvert.DeserializeObject(value, type);
        }
    }
}
