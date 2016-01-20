using System.Reflection;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public delegate bool ScanningStrategy(AssemblyName assembly);
}