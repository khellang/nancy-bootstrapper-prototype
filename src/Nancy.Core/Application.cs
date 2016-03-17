namespace Nancy.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core.Http;

    public abstract class Application<TContainer, TScope> : IApplication<TContainer>
    {
        protected Application(ConditionalDisposable<TContainer> container)
        {
            Check.NotNull(container, nameof(container));

            this.Container = container;
        }

        TContainer IApplication<TContainer>.Container => this.Container.Value;

        private ConditionalDisposable<TContainer> Container { get; }

        public Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            Check.NotNull(context, nameof(context));

            return this.HandleRequestInternal(context, cancellationToken);
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }

        protected virtual bool TryGetExistingScope(HttpContext context, out TScope provider)
        {
            provider = default(TScope);
            return false;
        }

        protected abstract TScope BeginRequestScope(HttpContext context, TContainer container);

        protected abstract IEngine ComposeEngine(TContainer container, TScope scope);

        private async Task HandleRequestInternal(HttpContext context, CancellationToken cancellationToken)
        {
            using (var scope = this.GetRequestScope(context))
            {
                var engine = this.ComposeEngineSafely(this.Container.Value, scope.Value);

                await engine.HandleRequest(context, cancellationToken).ConfigureAwait(false);
            }
        }

        private ConditionalDisposable<TScope> GetRequestScope(HttpContext context)
        {
            TScope scope;
            if (this.TryGetExistingScope(context, out scope))
            {
                // We don't want to dispose an existing scope, that's out of our control.
                return new ConditionalDisposable<TScope>(scope, shouldDispose: false);
            }

            var newScope = this.BeginRequestScope(context, this.Container.Value);

            // We've created this scope, make sure we dispose it.
            return new ConditionalDisposable<TScope>(newScope, shouldDispose: true);
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
    {
        protected Application(ConditionalDisposable<TContainer> container) : base(container)
        {
        }

        protected sealed override IEngine ComposeEngine(TContainer container, TContainer scope)
        {
            return this.ComposeEngine(scope);
        }

        protected abstract IEngine ComposeEngine(TContainer container);
    }
}
