using System.Collections.Generic;
using System.Reflection;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public interface IAssemblyCatalog
    {
        IReadOnlyCollection<Assembly> GetAssemblies(ScanningStrategy strategy);
    }
}