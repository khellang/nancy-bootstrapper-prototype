namespace Nancy.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core.Http;

    public abstract class Application<TContainer, TScope> : IApplication<TContainer>
        where TContainer : class, IDisposable
        where TScope : class, IDisposable
    {
        protected Application(Disposable<TContainer> container)
        {
            Check.NotNull(container, nameof(container));

            this.Container = container;
        }

        TContainer IApplication<TContainer>.Container => this.Container;

        private Disposable<TContainer> Container { get; }

        public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            Check.NotNull(context, nameof(context));

            return this.HandleRequestInternal(context, cancellationToken);
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }

        protected virtual bool TryGetExistingScope(HttpContext context, out TScope scope)
        {
            scope = default(TScope);
            return false;
        }

        protected abstract TScope BeginRequestScope(HttpContext context, TContainer container);

        protected abstract IEngine ComposeEngine(TContainer container, TScope scope);

        private async Task HandleRequestInternal(HttpContext context, CancellationToken cancellationToken)
        {
            using (var scope = this.GetRequestScope(context))
            {
                var engine = this.ComposeEngineSafely(this.Container, scope);

                await engine.HandleRequest(context, cancellationToken).ConfigureAwait(false);
            }
        }

        private Disposable<TScope> GetRequestScope(HttpContext context)
        {
            if (this.TryGetExistingScope(context, out var existingScope))
            {
                // We don't want to dispose an existing scope, that's out of our control.
                return new NonDisposable<TScope>(existingScope);
            }

            var scope = this.BeginRequestScope(context, this.Container);

            // We've created this scope, make sure we dispose it.
            return new Disposable<TScope>(scope);
        }

        private IEngine ComposeEngineSafely(TContainer container, TScope scope)
        {
            try
            {
                return this.ComposeEngine(container, scope);
            }
            catch (Exception ex)
            {
                throw new EngineCompositionException(Resources.Exception_EngineComposition, ex);
            }
        }
    }

    public abstract class Application<TContainer> : Application<TContainer, TContainer>
        where TContainer : class, IDisposable
    {
        protected Application(Disposable<TContainer> container) : base(container)
        {
        }

        protected sealed override IEngine ComposeEngine(TContainer container, TContainer scope)
        {
            return this.ComposeEngine(scope);
        }

        protected abstract IEngine ComposeEngine(TContainer container);
    }
}
