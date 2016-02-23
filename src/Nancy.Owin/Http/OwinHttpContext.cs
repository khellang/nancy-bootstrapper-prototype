namespace Nancy.Owin.Http
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;
    using Nancy.Core.Http;

    internal sealed class OwinHttpContext : HttpContext
    {
        private readonly IDictionary<string, object> environment;

        public OwinHttpContext(IDictionary<string, object> environment)
        {
            this.environment = environment;
            this.Request = new OwinHttpRequest(this, environment);
            this.Response = new OwinHttpResponse(this, environment);
            this.Items = new ItemsDictionary();
        }

        public override HttpRequest Request { get; }

        public override HttpResponse Response { get; }

        public override ClaimsPrincipal User
        {
            get { return GetUser(this.environment); }
            set { SetUser(this.environment, value); }
        }

        public override IDictionary<object, object> Items { get; }

        private static ClaimsPrincipal GetUser(IDictionary<string, object> environment)
        {
            var requestUser = environment.Get<ClaimsPrincipal>(Constants.RequestUser);

            if (requestUser == null)
            {
                var serverUser = environment.Get<IPrincipal>(Constants.ServerUser);

                if (serverUser == null)
                {
                    return null;
                }

                return ConvertToClaimsPrincipal(serverUser);
            }

            return requestUser;
        }

        private static ClaimsPrincipal ConvertToClaimsPrincipal(IPrincipal principal)
        {
            return principal as ClaimsPrincipal ?? new ClaimsPrincipal(principal);
        }

        private static void SetUser(IDictionary<string, object> environment, ClaimsPrincipal principal)
        {
            // We set both the official owin.RequestUser and the unofficial server.User.
            environment.Set(Constants.ServerUser, principal);
            environment.Set(Constants.RequestUser, principal);
        }
    }
}
