using System;
using Nancy.Core.Configuration;
using Nancy.Core.Registration;

namespace Nancy.Core
{
    /// <summary>
    /// The main base class for all bootstrappers.
    ///
    /// This is responsible for creating and configuring the
    /// application container and the framework as a whole.
    /// </summary>
    public abstract class Bootstrapper<TBuilder, TContainer> : IBootstrapper<TBuilder, TContainer>
        where TContainer : IDisposable
    {
        public IApplication InitializeApplication()
        {
            // First, we need a container builder.
            // This step is a noop in bootstrappers without the builder/container split.
            var builder = CreateBuilder();

            Populate(builder);

            // Once everything is registered, it's time to build the container.
            // This step is a noop in bootstrappers without the builder/container split.
            var container = BuildContainer(builder);

            return InitializeApplication(container);
        }

        public void Populate(TBuilder builder)
        {
            var frameworkConfig = new FrameworkConfiguration();

            // We'll hang all configuration related stuff off this object.
            // Everything will be pre-configured with Nancy defaults.
            var applicationConfig = new ApplicationConfiguration<TBuilder>(builder, frameworkConfig);

            // This is the main configuration point for the user.
            // Here you can register stuff in the container, swap out
            // Nancy services, change configuration etc.
            ConfigureApplication(applicationConfig);

            // Get platform services to register in the container.
            var platformRegistry = PlatformServices.Default.GetRegistry();

            Register(builder, platformRegistry);

            // Once the user has configured everything, we build a
            // "container registry", this contains all registrations
            // for framework services.
            var frameworkRegistry = frameworkConfig.GetRegistry(PlatformServices.Default.TypeCatalog);

            // We then call out to the bootstrapper implementation
            // to register all the registrations in the registry.
            Register(builder, frameworkRegistry);
        }

        public IApplication InitializeApplication(TContainer container)
        {
            // When the container is built, we offer the bootstrapper
            // implementation a chance to validate the container configuration
            // This could prevent obvious configuration errors.
            ValidateContainerConfiguration(container);

            // We finally ask the bootstrapper implementation to give us
            // an IApplication instance before returning it to the caller.
            return CreateApplication(container);
        }

        protected abstract TBuilder CreateBuilder();

        protected virtual void ConfigureApplication(IApplicationConfiguration<TBuilder> app)
        {
        }

        protected abstract void Register(TBuilder builder, IContainerRegistry registry);

        protected abstract TContainer BuildContainer(TBuilder builder);

        protected abstract void ValidateContainerConfiguration(TContainer container);

        protected abstract IApplication CreateApplication(TContainer container);
    }

    /// <summary>
    /// A convenience bootstrapper base for containers without a builder/container
    /// split, i.e. they allow appending to an existing container instance.
    /// </summary>
    public abstract class Bootstrapper<TContainer> : Bootstrapper<TContainer, TContainer>
        where TContainer : IDisposable
    {
        protected sealed override TContainer CreateBuilder() => CreateContainer();

        protected sealed override TContainer BuildContainer(TContainer container) => container;

        protected abstract TContainer CreateContainer();
    }
}