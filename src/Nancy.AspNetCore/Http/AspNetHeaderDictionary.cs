namespace Nancy.AspNetCore.Http
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Primitives;
    using Nancy.Core.Http;

    internal class AspNetHeaderDictionary : IHeaderDictionary
    {
        private readonly Microsoft.AspNetCore.Http.IHeaderDictionary headers;

        public AspNetHeaderDictionary(Microsoft.AspNetCore.Http.IHeaderDictionary headers)
        {
            this.headers = headers;
        }

        public string[] this[string key]
        {
            get { return this.headers[key]; }
            set { this.headers[key] = value; }
        }

        public int Count => this.headers.Count;

        public bool IsReadOnly => this.headers.IsReadOnly;

        public ICollection<string> Keys => this.headers.Keys;

        public ICollection<string[]> Values => this.headers.Values.Select(x => x.ToArray()).ToArray();

        public void Clear() => this.headers.Clear();

        public bool Remove(string key) => this.headers.Remove(key);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public bool ContainsKey(string key) => this.headers.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, string[]>[] array, int arrayIndex)
        {
            foreach (var pair in this.headers)
            {
                array[arrayIndex++] = Convert(pair);
            }
        }

        public void Add(string key, string[] value) => this.headers.Add(key, value);

        public void Add(KeyValuePair<string, string[]> item) => this.headers.Add(Convert(item));

        public bool Contains(KeyValuePair<string, string[]> item) => this.headers.Contains(Convert(item));

        public bool Remove(KeyValuePair<string, string[]> item) => this.headers.Remove(Convert(item));

        public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator() => this.headers.Select(Convert).GetEnumerator();

        public bool TryGetValue(string key, out string[] value)
        {
            if (this.headers.TryGetValue(key, out var temp))
            {
                value = temp;
                return true;
            }

            value = default(string[]);
            return false;
        }

        private static KeyValuePair<string, string[]> Convert(KeyValuePair<string, StringValues> item)
        {
            return new KeyValuePair<string, string[]>(item.Key, item.Value);
        }

        private static KeyValuePair<string, StringValues> Convert(KeyValuePair<string, string[]> item)
        {
            return new KeyValuePair<string, StringValues>(item.Key, item.Value);
        }
    }
}
