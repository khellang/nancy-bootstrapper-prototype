using System;
using System.Collections.Generic;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public interface ITypeCatalog
    {
        IReadOnlyCollection<Type> TypesOf(Type type, ScanMode scanMode);
    }
}