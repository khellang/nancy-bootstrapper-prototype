namespace Nancy.Owin.Http
{
    using System.Collections;
    using System.Collections.Generic;

    internal sealed class ItemsDictionary : IDictionary<object, object>
    {
        private readonly IDictionary<object, object> items = new Dictionary<object, object>();

        int ICollection<KeyValuePair<object, object>>.Count => this.items.Count;

        bool ICollection<KeyValuePair<object, object>>.IsReadOnly => this.items.IsReadOnly;

        ICollection<object> IDictionary<object, object>.Keys => this.items.Keys;

        ICollection<object> IDictionary<object, object>.Values => this.items.Values;

        object IDictionary<object, object>.this[object key]
        {
            get
            {
                object value;
                // We don't want the indexer to throw. Just return null.
                return this.items.TryGetValue(key, out value) ? value : null;
            }
            set { this.items[key] = value; }
        }

        bool ICollection<KeyValuePair<object, object>>.Remove(KeyValuePair<object, object> item)
        {
            object value;
            if (this.items.TryGetValue(item.Key, out value) && Equals(item.Value, value))
            {
                return this.items.Remove(item.Key);
            }

            return false;
        }

        void ICollection<KeyValuePair<object, object>>.Clear() => this.items.Clear();

        bool IDictionary<object, object>.Remove(object key) => this.items.Remove(key);

        bool IDictionary<object, object>.ContainsKey(object key) => this.items.ContainsKey(key);

        void IDictionary<object, object>.Add(object key, object value) => this.items.Add(key, value);

        void ICollection<KeyValuePair<object, object>>.Add(KeyValuePair<object, object> item) => this.items.Add(item);

        bool IDictionary<object, object>.TryGetValue(object key, out object value) => this.items.TryGetValue(key, out value);

        bool ICollection<KeyValuePair<object, object>>.Contains(KeyValuePair<object, object> item) => this.items.ContainsKey(item);

        void ICollection<KeyValuePair<object, object>>.CopyTo(KeyValuePair<object, object>[] array, int arrayIndex) => this.items.CopyTo(array, arrayIndex);

        IEnumerator<KeyValuePair<object, object>> IEnumerable<KeyValuePair<object, object>>.GetEnumerator() => this.items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.items.GetEnumerator();
    }
}
