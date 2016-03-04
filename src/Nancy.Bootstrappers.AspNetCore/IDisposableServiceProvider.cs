namespace Nancy.Bootstrappers.AspNetCore
{
    using System;

    /// <summary>
    ///     Interface to make up for the missing IDisposable
    ///     on the good old IServiceProvider interface.
    /// </summary>
    /// <remarks>
    ///     Without this we'd have to drop the IDisposable generic constraint.
    /// </remarks>
    public interface IDisposableServiceProvider : IServiceProvider, IDisposable
    {
    }
}
