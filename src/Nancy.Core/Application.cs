namespace Nancy.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core.Http;

    public abstract class Application<TContainer, TScope> : IApplication<TContainer>
    {
        private readonly bool shouldDispose;

        protected Application(TContainer container, bool shouldDispose)
        {
            Check.NotNull(container, nameof(container));

            this.Container = container;
            this.shouldDispose = shouldDispose;
        }

        public TContainer Container { get; }

        public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            Check.NotNull(context, nameof(context));

            return this.HandleRequestInternal(context, cancellationToken);
        }

        public void Dispose()
        {
            if (this.shouldDispose)
            {
                TryDispose(this.Container);
            }
        }

        protected abstract TScope BeginRequestScope(HttpContext context, TContainer container);

        protected abstract IEngine ComposeEngine(TContainer container, TScope scope);

        private async Task HandleRequestInternal(HttpContext context, CancellationToken cancellationToken)
        {
            var scope = this.BeginRequestScope(context, this.Container);

            try
            {
                var engine = this.ComposeEngineSafely(this.Container, scope);

                await engine.HandleRequest(context, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                TryDispose(scope);
            }
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

        private static void TryDispose(object @object)
        {
            (@object as IDisposable)?.Dispose();
        }
    }

    public abstract class Application<TContainer> : Application<TContainer, TContainer>
    {
        protected Application(TContainer container, bool shouldDispose)
            : base(container, shouldDispose)
        {
        }

        protected sealed override IEngine ComposeEngine(TContainer container, TContainer scope)
        {
            return this.ComposeEngine(scope);
        }

        protected abstract IEngine ComposeEngine(TContainer container);
    }
}
