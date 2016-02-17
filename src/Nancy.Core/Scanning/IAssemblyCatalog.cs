namespace Nancy.Core.Scanning
{
    using System.Collections.Generic;
    using System.Reflection;

    public interface IAssemblyCatalog
    {
        IReadOnlyCollection<Assembly> GetAssemblies();
    }
}
