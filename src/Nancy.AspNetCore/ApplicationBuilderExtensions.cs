﻿namespace Nancy.AspNetCore
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Nancy.AspNetCore.Http;
    using Nancy.Bootstrappers.AspNetCore;
    using Nancy.Core;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNancy(this IApplicationBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            var provider = builder.ApplicationServices.AsDisposable();

            var bootstrapper = provider.GetService<IBootstrapper<IDisposableServiceProvider>>();

            if (bootstrapper == null)
            {
                throw new InvalidOperationException(Resources.Exception_MustCallAddNancy);
            }

            var application = bootstrapper.InitializeApplication(provider);

            return builder.UseMiddleware<NancyMiddleware>(application);
        }

        private class NancyMiddleware
        {
            private readonly RequestDelegate next;

            private readonly IApplication application;

            public NancyMiddleware(RequestDelegate next, IApplication application)
            {
                this.next = next;
                this.application = application;
            }

            public Task Invoke(HttpContext context)
            {
                var httpContext = new AspNetHttpContext(context);

                // Add the request services so we can get it out in the bootstrapper.
                httpContext.Items.Add(Constants.AspNetRequestServices, context.RequestServices);

                var cancellationToken = context.RequestAborted;

                return this.application.HandleRequest(httpContext, cancellationToken);
            }
        }
    }
}
