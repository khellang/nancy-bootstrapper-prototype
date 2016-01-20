using System.Reflection;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public static class ScanningStrategies
    {
        private static readonly AssemblyName NancyAssemblyName = typeof(IEngine).GetTypeInfo().Assembly.GetName();

        public static readonly ScanningStrategy All = assemblyName => true;

        public static readonly ScanningStrategy OnlyNancy = assemblyName => assemblyName.Equals(NancyAssemblyName);

        public static readonly ScanningStrategy ExcludeNancy = assemblyName => !assemblyName.Equals(NancyAssemblyName);
    }
}