using System;
using System.Collections.Generic;

namespace Nancy.Bootstrapper.Prototype.Scanning
{
    public interface ITypeCatalog
    {
        IReadOnlyCollection<Type> GetTypesAssignableTo(Type type, ScanMode scanMode);
    }
}