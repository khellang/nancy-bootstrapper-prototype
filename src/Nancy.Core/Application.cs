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
        private readonly bool shouldDispose;

        protected Application(TContainer container, bool shouldDispose)
        {
            Check.NotNull(container, nameof(container));

            this.Container = container;
            this.shouldDispose = shouldDispose;
        }

        public TContainer Container { get; }

        public async Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            Check.NotNull(context, nameof(context));

            using (var scope = this.BeginRequestScope(context, this.Container))
            {
                var engine = this.ComposeEngine(this.Container, scope);

                await engine.HandleRequest(context, cancellationToken).ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            if (this.shouldDispose)
            {
                this.Container.Dispose();
            }
        }

        protected abstract TScope BeginRequestScope(HttpContext context, TContainer container);

        protected abstract IEngine ComposeEngine(TContainer container, TScope scope);
    }

    public abstract class Application<TContainer> : Application<TContainer, TContainer>
        where TContainer : class, IDisposable
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
