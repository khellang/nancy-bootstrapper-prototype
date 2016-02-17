namespace Nancy.Core
{
    using System;

    internal static class Check
    {
        public static void NotNull<T>(T value, string paramName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
