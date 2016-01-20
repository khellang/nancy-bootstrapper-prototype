namespace Nancy.Core
{
    public interface IBootstrapperLocator
    {
        IBootstrapper GetBootstrapper();
    }
}