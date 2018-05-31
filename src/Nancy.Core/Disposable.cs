namespace Nancy.Core
{
    using System;

    public class Disposable<T> : IDisposable where T : class, IDisposable
    {
        public Disposable(T instance)
        {
            this.Instance = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        public T Instance { get; }

        public static implicit operator T(Disposable<T> disposable)
        {
            return disposable.Instance;
        }

        public virtual void Dispose()
        {
            this.Instance.Dispose();
        }
    }
}
