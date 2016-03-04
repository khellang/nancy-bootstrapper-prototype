namespace Nancy.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Nancy.Core.Scanning;

    internal class StaticTypeCatalog : ITypeCatalog
    {
        private readonly Type[] types;

        public StaticTypeCatalog(params Type[] types)
        {
            this.types = types;
        }

        public IEnumerable<Type> GetTypesAssignableTo(Type type, ScanningStrategy strategy)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(strategy, nameof(strategy));

            var typeInfo = type.GetTypeInfo();

            foreach (var catalogType in this.types)
            {
                var catalogTypeInfo = catalogType.GetTypeInfo();

                if (!catalogTypeInfo.IsAssignableTo(typeInfo))
                {
                    continue;
                }

                if (strategy.Invoke(catalogTypeInfo.Assembly))
                {
                    yield return catalogType;
                }
            }
        }
    }
}
