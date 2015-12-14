namespace Nancy.Bootstrapper.Prototype.Cruft
{
    public interface IApplicationBuilder<out TContainer>
    {
        TContainer Container { get; }
    }
}