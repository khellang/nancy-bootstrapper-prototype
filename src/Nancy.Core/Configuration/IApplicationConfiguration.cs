namespace Nancy.Bootstrapper.Prototype.Configuration
{
    public interface IApplicationConfiguration<out TContainer>
    {
        TContainer Container { get; }

        IFrameworkConfiguration Framework { get; }
    }
}