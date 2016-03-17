namespace Nancy.Bootstrappers.AspNetCore
{
    using System;

    public interface IDisposableServiceProvider : IServiceProvider, IDisposable
    {
    }
}
