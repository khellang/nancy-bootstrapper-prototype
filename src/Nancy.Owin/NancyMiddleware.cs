using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nancy.Owin
{
    using System.Threading;
    using Nancy.Core;
    using Nancy.Owin.Http;

    using MidFunc = Func<
        Func<IDictionary<string, object>, Task>,
        Func<IDictionary<string, object>, Task>>;

    public static class NancyMiddleware
    {
        public static MidFunc Create(IApplication application)
        {
            Check.NotNull(application, nameof(application));

            return next => environment =>
            {
                var httpContext = new OwinHttpContext(environment);

                var callCancelled = environment.Get<CancellationToken>(Constants.CallCancelled);

                return application.HandleRequest(httpContext, callCancelled);
            };
        }
    }
}
