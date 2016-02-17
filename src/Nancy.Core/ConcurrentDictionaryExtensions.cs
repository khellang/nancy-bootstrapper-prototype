namespace Nancy.Core
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    ///     TODO: Remove this class when https://github.com/dotnet/corefx/pull/1783 is available.
    /// </summary>
    internal static class ConcurrentDictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue, TArg>(
            this ConcurrentDictionary<TKey, TValue> dictionary,
            TKey key,
            TArg arg,
            Func<TKey, TArg, TValue> valueFactory)
        {
            Check.NotNull(dictionary, nameof(dictionary));
            Check.NotNull(key, nameof(key));
            Check.NotNull(arg, nameof(arg));
            Check.NotNull(valueFactory, nameof(valueFactory));

            while (true)
            {
                TValue value;
                if (dictionary.TryGetValue(key, out value))
                {
                    return value;
                }

                value = valueFactory(key, arg);

                if (dictionary.TryAdd(key, value))
                {
                    return value;
                }
            }
        }
    }
}
