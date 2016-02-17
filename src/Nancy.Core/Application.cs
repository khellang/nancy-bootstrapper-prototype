namespace Nancy.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy.Core.Http;

    public abstract class Application<TContainer, TScope> : IApplication
        where TContainer : IDisposable
        where TScope : IDisposable
    {
        private readonly TContainer container;

        private readonly bool shouldDispose;

        protected Application(TContainer container, bool shouldDispose)
        {
            this.container = container;
            this.shouldDispose = shouldDispose;
        }

        public async Task HandleRequest(HttpContext context, CancellationToken cancellationToken)
        {
            using (var scope = this.BeginRequestScope(context, this.container))
            {
                var engine = this.ComposeEngine(this.container, scope);

                await engine.HandleRequest(context, cancellationToken).ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            if (this.shouldDispose)
            {
                this.container.Dispose();
            }
        }

        protected abstract TScope BeginRequestScope(HttpContext context, TContainer container);

        protected abstract IEngine ComposeEngine(TContainer container, TScope scope);
    }

    public abstract class Application<TContainer> : Application<TContainer, TContainer>
        where TContainer : IDisposable
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
