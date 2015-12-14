using System;
using System.Collections.Generic;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public interface ITypeCatalog
    {
        IEnumerable<Type> TypesOf(Type type, ScanMode scanMode);
    }
}