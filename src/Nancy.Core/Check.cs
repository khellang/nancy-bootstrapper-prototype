namespace Nancy.Core
{
    using System;

    public static class Check
    {
        public static void NotNull<T>(T value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
