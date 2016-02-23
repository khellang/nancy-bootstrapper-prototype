namespace Nancy.Owin
{
    using System.Collections.Generic;
    using Nancy.Core;

    internal static class OwinEnvironmentExtensions
    {
        public static T Get<T>(this IDictionary<string, object> environment, string key)
        {
            Check.NotNull(environment, nameof(environment));
            Check.NotNull(key, nameof(key));

            object value;
            if (environment.TryGetValue(key, out value) && value is T)
            {
                return (T) value;
            }

            return default(T);
        }

        public static void Set<T>(this IDictionary<string, object> environment, string key, T value)
        {
            Check.NotNull(environment, nameof(environment));
            Check.NotNull(key, nameof(key));

            environment[key] = value;
        }
    }
}
