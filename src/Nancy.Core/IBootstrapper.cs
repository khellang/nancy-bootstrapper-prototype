namespace Nancy.Core
{
    public interface IBootstrapper
    {
        IApplication InitializeApplication(IPlatformServices platformServices);
    }

    public interface IBootstrapper<in TContainer> : IBootstrapper
    {
        IApplication InitializeApplication(TContainer container);
    }

    public interface IBootstrapper<in TBuilder, in TContainer> : IBootstrapper<TContainer>
    {
        IBootstrapper<TContainer> Populate(TBuilder builder, IPlatformServices platformServices);
    }
}
