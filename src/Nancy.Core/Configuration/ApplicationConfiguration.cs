namespace Nancy.Core.Configuration
{
    public class ApplicationConfiguration<TContainer> : IApplicationConfiguration<TContainer>
    {
        public ApplicationConfiguration(TContainer container, IFrameworkConfiguration framework)
        {
            this.Container = container;
            this.Framework = framework;
        }

        public TContainer Container { get; }

        public IFrameworkConfiguration Framework { get; }
    }
}
