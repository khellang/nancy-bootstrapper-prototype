namespace Nancy.Core
{
    using System;

    public sealed class NonDisposable<T> : Disposable<T> where T : class, IDisposable
    {
        public NonDisposable(T instance) : base(instance)
        {
        }

        public override void Dispose()
        {
            // Do nothing.
        }
    }
}
