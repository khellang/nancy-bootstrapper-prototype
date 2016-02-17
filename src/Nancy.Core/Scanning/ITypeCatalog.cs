namespace Nancy.Core.Scanning
{
    using System;
    using System.Collections.Generic;

    public interface ITypeCatalog
    {
        IReadOnlyCollection<Type> GetTypesAssignableTo(Type type, ScanningStrategy strategy);
    }
}
