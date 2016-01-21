using System;
using System.Reflection;

namespace Nancy.Core.Scanning
{
    public static class ScanningStrategies
    {
        public static readonly string NancyAssemblyName = typeof(IEngine).GetTypeInfo().Assembly.GetName().Name;

        public static readonly ScanningStrategy All = assemblyName => true;

        public static readonly ScanningStrategy OnlyNancy = assemblyName =>
            assemblyName.Name.Equals(NancyAssemblyName, StringComparison.Ordinal);

        public static readonly ScanningStrategy ExcludeNancy = assemblyName =>
            !assemblyName.Name.Equals(NancyAssemblyName, StringComparison.Ordinal);
    }
}