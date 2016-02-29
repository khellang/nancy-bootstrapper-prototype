namespace Nancy.Owin.Http
{
    using System.Collections;
    using System.Collections.Generic;
    using Nancy.Core;
    using Nancy.Core.Http;

    internal class OwinHeaderDictionary : IHeaderDictionary
    {
        private readonly IDictionary<string, string[]> headers;

        public OwinHeaderDictionary(IDictionary<string, string[]> headers)
        {
            Check.NotNull(headers, nameof(headers));

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

        public ICollection<string[]> Values => this.headers.Values;

        public void Clear() => this.headers.Clear();

        public bool Remove(string key) => this.headers.Remove(key);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public bool ContainsKey(string key) => this.headers.ContainsKey(key);

        public void Add(string key, string[] value) => this.headers.Add(key, value);

        public void Add(KeyValuePair<string, string[]> item) => this.headers.Add(item);

        public bool Remove(KeyValuePair<string, string[]> item) => this.headers.Remove(item);

        public bool Contains(KeyValuePair<string, string[]> item) => this.headers.Contains(item);

        public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator() => this.headers.GetEnumerator();

        public bool TryGetValue(string key, out string[] value) => this.headers.TryGetValue(key, out value);

        public void CopyTo(KeyValuePair<string, string[]>[] array, int arrayIndex) => this.headers.CopyTo(array, arrayIndex);
    }
}
