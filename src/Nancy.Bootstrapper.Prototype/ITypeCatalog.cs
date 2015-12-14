using System;
using System.Collections.Generic;

namespace Nancy.Bootstrapper.Prototype
{
    public interface ITypeCatalog
    {
        IEnumerable<Type> TypesOf(Type type, ScanMode scanMode);
    }
}