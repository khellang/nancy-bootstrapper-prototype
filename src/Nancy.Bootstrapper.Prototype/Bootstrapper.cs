using System;
using Nancy.Bootstrapper.Prototype.Configuration;
using Nancy.Bootstrapper.Prototype.Registration;
using Nancy.Bootstrapper.Prototype.Scanning;

namespace Nancy.Bootstrapper.Prototype
{
    /// <summary>
    /// The main base class for all bootstrappers.
    ///
    /// This is responsible for creating and configuring the
    /// application container and the framework as a whole.
    /// </summary>
    public abstract class Bootstrapper<TBuilder, TContainer> : IBootstrapper
        where TContainer : IDisposable
    {
        public IApplication InitializeApplication(IAssemblyCatalog assemblyCatalog, ITypeCatalog typeCatalog)
        {
            // First, we need a container builder.
            // This step is a noop in bootstrappers without the builder/container split.
            var builder = CreateBuilder();

            var frameworkConfig = new FrameworkConfiguration();

            // We'll hang all configuration related stuff off this object.
            // Everything will be pre-configured with Nancy defaults.
            var applicationConfig = new ApplicationConfiguration<TBuilder>(builder, frameworkConfig);

            // This is the main configuration point for the user.
            // Here you can register stuff in the container, swap out
            // Nancy services, change configuration etc.
            ConfigureApplication(applicationConfig);

            // Once the user has configured everything, we build a
            // "container registry", this contains all registrations
            // for framework services.
            var registry = frameworkConfig.GetRegistry(typeCatalog);

            // We then call out to the bootstrapper implementation
            // to register all the registrations in the registry.
            Register(builder, registry);

            // Once everything is registered, it's time to build the container.
            // This step is a noop in bootstrappers without the builder/container split.
            var container = BuildContainer(builder);

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

        protected virtual void ValidateContainerConfiguration(TContainer container)
        {
        }

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