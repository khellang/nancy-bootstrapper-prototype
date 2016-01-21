using System.Reflection;

namespace Nancy.Core.Scanning
{
    public static class ScanningStrategies
    {
        public static readonly AssemblyName NancyAssemblyName = typeof(IEngine).GetTypeInfo().Assembly.GetName();

        public static readonly ScanningStrategy All = assemblyName => true;

        public static readonly ScanningStrategy OnlyNancy = assemblyName => assemblyName.Equals(NancyAssemblyName);

        public static readonly ScanningStrategy ExcludeNancy = assemblyName => !assemblyName.Equals(NancyAssemblyName);
    }
}