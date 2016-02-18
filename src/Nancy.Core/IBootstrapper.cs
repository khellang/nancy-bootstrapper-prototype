namespace Nancy.Core
{
    public interface IBootstrapper
    {
        IApplication InitializeApplication(IPlatformServices platformServices);
    }

    public interface IBootstrapper<TContainer> : IBootstrapper
    {
        IApplication<TContainer> InitializeApplication(TContainer container);
    }

    public interface IBootstrapper<in TBuilder, TContainer> : IBootstrapper<TContainer>
    {
        IBootstrapper<TContainer> Populate(TBuilder builder, IPlatformServices platformServices);
    }
}
