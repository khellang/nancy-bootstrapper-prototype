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

            if (environment.TryGetValue(key, out var value) && value is T variable)
            {
                return variable;
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
