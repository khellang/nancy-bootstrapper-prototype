namespace Nancy.Core
{
    using System;

    public class ConditionalDisposable<T> : IDisposable
    {
        private readonly bool shouldDispose;

        public ConditionalDisposable(T value, bool shouldDispose)
        {
            Check.NotNull(value, nameof(value));

            this.Value = value;
            this.shouldDispose = shouldDispose;
        }

        public T Value { get; }

        public void Dispose()
        {
            if (this.shouldDispose)
            {
                (this.Value as IDisposable)?.Dispose();
            }
        }
    }
}
