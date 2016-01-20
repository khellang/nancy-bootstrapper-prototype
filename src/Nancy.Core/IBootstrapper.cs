namespace Nancy.Core
{
    public interface IBootstrapper
    {
        IApplication InitializeApplication();
    }

    public interface IBootstrapper<in TBuilder, in TContainer> : IBootstrapper
    {
        void Populate(TBuilder builder);

        IApplication InitializeApplication(TContainer container);
    }
}