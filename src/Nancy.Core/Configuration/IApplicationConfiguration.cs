namespace Nancy.Core.Configuration
{
    public interface IApplicationConfiguration<out TContainer>
    {
        TContainer Container { get; }

        IFrameworkConfiguration Framework { get; }
    }
}