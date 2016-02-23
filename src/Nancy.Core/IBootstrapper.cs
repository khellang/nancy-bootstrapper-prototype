namespace Nancy.Core
{
    public interface IBootstrapper
    {
        /// <summary>
        /// Initializes the application.
        /// This method creates an internal container with all the required services.
        /// </summary>
        /// <param name="platformServices">The platform services.</param>
        IApplication InitializeApplication(IPlatformServices platformServices);
    }

    public interface IBootstrapper<TContainer> : IBootstrapper
    {
        /// <summary>
        /// Initializes the application using an external, pre-built container.
        /// This method requires you to call <see cref="IBootstrapper{TBuilder,TContainer}.Populate"/>
        /// to make sure all required services are added to the container.
        /// </summary>
        /// <param name="container">The external, pre-built container.</param>
        IApplication<TContainer> InitializeApplication(TContainer container);
    }

    public interface IBootstrapper<in TBuilder, TContainer> : IBootstrapper<TContainer>
    {
        /// <summary>
        /// Populates the specified container builder with the required services.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        /// <param name="platformServices">The platform services.</param>
        IBootstrapper<TContainer> Populate(TBuilder builder, IPlatformServices platformServices);
    }
}
