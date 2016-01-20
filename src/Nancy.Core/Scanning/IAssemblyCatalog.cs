using System.Collections.Generic;
using System.Reflection;

namespace Nancy.Core.Scanning
{
    public interface IAssemblyCatalog
    {
        IReadOnlyCollection<Assembly> GetAssemblies(ScanningStrategy strategy);
    }
}