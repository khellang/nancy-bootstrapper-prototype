namespace Nancy.Core
{
    public interface IBootstrapper
    {
        IApplication InitializeApplication();
    }

    public interface IBootstrapper<in TBuilder, in TContainer> : IBootstrapper<TContainer>
    {
        void Populate(TBuilder builder);
    }

    public interface IBootstrapper<in TContainer> : IBootstrapper
    {
        IApplication InitializeApplication(TContainer container);
    }
}