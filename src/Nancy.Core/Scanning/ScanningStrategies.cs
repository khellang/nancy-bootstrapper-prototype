namespace Nancy.Core.Scanning
{
    using System;
    using System.Reflection;

    public static class ScanningStrategies
    {
        public static readonly Assembly NancyAssembly = typeof(IEngine).GetTypeInfo().Assembly;

        public static ScanningStrategy All { get; } = assembly => true;

        public static ScanningStrategy OnlyNancy { get; } = assembly => IsNancy(assembly);

        public static ScanningStrategy ExcludeNancy { get; } = assembly => !IsNancy(assembly);

        private static bool IsNancy(Assembly assembly)
        {
            return assembly.Equals(NancyAssembly);
        }
    }
}
