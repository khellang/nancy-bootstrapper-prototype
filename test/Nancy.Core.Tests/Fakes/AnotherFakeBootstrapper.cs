namespace Nancy.Core.Tests.Fakes
{
    using Nancy.Core.Registration;

    public class AnotherFakeBootstrapper : Bootstrapper<AnotherFakeContainer>
    {
        protected override void Register(AnotherFakeContainer builder, IContainerRegistry registry)
        {
        }

        protected override void ValidateContainerConfiguration(AnotherFakeContainer container)
        {
        }

        protected override IApplication<AnotherFakeContainer> CreateApplication(AnotherFakeContainer container, bool shouldDispose)
        {
            return null;
        }

        protected override AnotherFakeContainer CreateContainer()
        {
            return null;
        }
    }
}