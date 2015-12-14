namespace ConsoleApplication7.Cruft
{
    public interface IApplicationBuilder<out TContainer>
    {
        TContainer Container { get; }
    }
}