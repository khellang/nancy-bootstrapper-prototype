namespace Nancy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Nancy.Core.Scanning;

    public class BootstrapperLocator : IBootstrapperLocator
    {
        private readonly ITypeCatalog typeCatalog;

        public BootstrapperLocator(ITypeCatalog typeCatalog)
        {
            Check.NotNull(typeCatalog, nameof(typeCatalog));

            this.typeCatalog = typeCatalog;
        }

        public IBootstrapper GetBootstrapper()
        {
            var types = this.typeCatalog
                .GetTypesAssignableTo<IBootstrapper>(ScanningStrategies.ExcludeNancy)
                .ToArray();

            var type = GetBootstrapperType(types);

            // TODO: Wrap potential exception with a better error message.
            return (IBootstrapper) Activator.CreateInstance(type);
        }

        private static Type GetBootstrapperType(Type[] types)
        {
            if (types.Length == 0)
            {
                // TODO: Return default bootstrapper.
                throw new InvalidOperationException("Could not locate a bootstrapper implementation.");
            }

            if (types.Length == 1)
            {
                return types[0];
            }

            Type type;
            if (TryFindMostDerivedBootstrapperType(types, out type))
            {
                return type;
            }

            var message = GenerateMultipleBootstrappersMessage(types);

            throw new InvalidOperationException(message);
        }

        private static bool TryFindMostDerivedBootstrapperType(Type[] types, out Type type)
        {
            var baseTypes = GetBaseTypes(types);

            var candidates = types.Except(baseTypes).ToArray();

            if (candidates.Length == 1)
            {
                type = candidates[0];
                return true;
            }

            type = null;
            return false;
        }

        private static IEnumerable<Type> GetBaseTypes(IEnumerable<Type> types)
        {
            var baseTypes = new HashSet<Type>();

            foreach (var type in types)
            {
                var baseType = type.GetTypeInfo().BaseType;

                if (baseType == null)
                {
                    continue;
                }

                baseTypes.Add(baseType);
            }

            return baseTypes;
        }

        private static string GenerateMultipleBootstrappersMessage(IEnumerable<Type> types)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Located multiple bootstrappers:");

            foreach (var type in types)
            {
                builder.AppendLine($" - {type.FullName}");
            }

            builder.AppendLine();
            builder.AppendLine("Either remove unused bootstrapper types or specify which type to use.");

            return builder.ToString();
        }
    }
}
