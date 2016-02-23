﻿namespace Nancy.Owin.Http
{
    using System.Collections.Generic;
    using System.IO;
    using Nancy.Core.Http;

    internal sealed class OwinHttpRequest : HttpRequest
    {
        private readonly IDictionary<string, object> environment;

        private readonly OwinUrl owinUrl;

        public OwinHttpRequest(HttpContext context, IDictionary<string, object> environment)
        {
            this.Context = context;
            this.environment = environment;
            this.owinUrl = new OwinUrl(this, environment);
        }

        public override HttpContext Context { get; }

        public override HttpMethod Method
        {
            get { return this.environment.Get<string>(Constants.RequestMethod); }
            set { this.environment.Set(Constants.RequestMethod, value.Value); }
        }

        public override Url Url
        {
            get { return this.owinUrl; }
            set { this.owinUrl.CopyFrom(value); }
        }

        public override string Protocol
        {
            get { return this.environment.Get<string>(Constants.RequestProtocol); }
            set { this.environment.Set(Constants.RequestProtocol, value); }
        }

        public override Stream Body
        {
            get { return this.environment.Get<Stream>(Constants.RequestBody); }
            set { this.environment.Set(Constants.RequestBody, value); }
        }
    }
}
