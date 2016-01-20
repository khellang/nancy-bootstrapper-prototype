using System;
using System.Collections.Generic;

namespace Nancy.Core.Scanning
{
    public interface ITypeCatalog
    {
        IReadOnlyCollection<Type> GetTypesAssignableTo(Type type, ScanningStrategy strategy);
    }
}