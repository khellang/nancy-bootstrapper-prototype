namespace Nancy.Core.Configuration
{
    internal class ApplicationConfiguration<TContainer> : IApplicationConfiguration<TContainer>
        where TContainer : class
    {
        public ApplicationConfiguration(TContainer container, IFrameworkConfiguration framework)
        {
            Check.NotNull(container, nameof(container));
            Check.NotNull(framework, nameof(framework));

            this.Container = container;
            this.Framework = framework;
        }

        public TContainer Container { get; }

        public IFrameworkConfiguration Framework { get; }
    }
}
