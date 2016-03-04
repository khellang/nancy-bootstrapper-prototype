namespace Nancy.Core.Tests
{
    using System;
    using Nancy.Core.Tests.Fakes;
    using Xunit;

    public class BootstrapperLocatorTests
    {
        [Fact]
        public void Should_throw_if_no_bootstrapper_is_found()
        {
            // Given
            var locator = GetLocator(Type.EmptyTypes);

            // When, Then
            Assert.Throws<InvalidOperationException>(() => locator.GetBootstrapper());
        }

        [Fact]
        public void Should_return_bootstrapper_if_only_one_is_found()
        {
            // Given
            var locator = GetLocator(typeof(FakeBootstrapper));

            // When
            var bootstrapper = locator.GetBootstrapper();

            // Then
            Assert.NotNull(bootstrapper);
            Assert.IsType<FakeBootstrapper>(bootstrapper);
        }

        [Fact]
        public void Should_return_most_derived_bootstrapper_if_multiple_are_found()
        {
            // Given
            var locator = GetLocator(typeof(FakeBootstrapper),
                typeof(DerivedFakeBootstrapper),
                typeof(MostDerivedFakeBootstrapper));

            // When
            var bootstrapper = locator.GetBootstrapper();

            // Then
            Assert.NotNull(bootstrapper);
            Assert.IsType<MostDerivedFakeBootstrapper>(bootstrapper);
        }

        [Fact]
        public void Should_throw_if_multiple_bootstrapper_types_are_found()
        {
            // Given
            var locator = GetLocator(typeof(FakeBootstrapper), typeof(AnotherFakeBootstrapper));

            // When, Then
            var exception = Assert.Throws<InvalidOperationException>(() => locator.GetBootstrapper());

            Assert.Contains(typeof(FakeBootstrapper).FullName, exception.Message);
            Assert.Contains(typeof(AnotherFakeBootstrapper).FullName, exception.Message);
        }

        [Fact]
        public void Should_throw_if_bootstrapper_does_not_have_a_default_constructor()
        {
            // Given
            var locator = GetLocator(typeof(FakeBootstrapperWithoutEmptyConstructor));

            // When, Then
            Assert.Throws<MissingMethodException>(() => locator.GetBootstrapper());
        }

        private static IBootstrapperLocator GetLocator(params Type[] types)
        {
            var typeCatalog = new StaticTypeCatalog(types);

            return new BootstrapperLocator(typeCatalog);
        }
    }
}
