using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype
{
    public interface IApplicationBuilder<out TContainer>
    {
        TContainer Container { get; }

        ITypeCatalog TypeCatalog { get; }
    }
}