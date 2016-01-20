namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public class ApplicationConfiguration<TContainer> : IApplicationConfiguration<TContainer>
    {
        public ApplicationConfiguration(TContainer container, IFrameworkConfiguration framework)
        {
            Container = container;
            Framework = framework;
        }

        public TContainer Container { get; }

        public IFrameworkConfiguration Framework { get; }
    }
}