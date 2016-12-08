namespace Nancy.Core.Tests.Fakes
{
    public class FakeBootstrapperWithoutEmptyConstructor : FakeBootstrapper
    {
        public FakeBootstrapperWithoutEmptyConstructor(string hello)
        {
        }
    }
}
