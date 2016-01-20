using System.Reflection;

namespace Nancy.Core.Scanning
{
    public delegate bool ScanningStrategy(AssemblyName assembly);
}