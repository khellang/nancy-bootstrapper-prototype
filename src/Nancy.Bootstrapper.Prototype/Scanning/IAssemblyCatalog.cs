using System.Collections.Generic;
using System.Reflection;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public interface IAssemblyCatalog
    {
        IEnumerable<Assembly> GetAssemblies(ScanMode scanMode);
    }
}