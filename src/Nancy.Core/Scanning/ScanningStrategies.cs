namespace Nancy.Core.Scanning
{
    using System;
    using System.Reflection;

    public static class ScanningStrategies
    {
        public static readonly string NancyAssemblyName = typeof(IEngine).GetTypeInfo().Assembly.GetName().Name;

        public static bool All(AssemblyName assemblyName)
        {
            return true;
        }

        public static bool OnlyNancy(AssemblyName assemblyName)
        {
            return assemblyName.Name.Equals(NancyAssemblyName, StringComparison.Ordinal);
        }

        public static bool ExcludeNancy(AssemblyName assemblyName)
        {
            return !assemblyName.Name.Equals(NancyAssemblyName, StringComparison.Ordinal);
        }
    }
}
