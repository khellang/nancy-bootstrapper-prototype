﻿namespace Nancy.Owin
{
    internal static class Constants
    {
        public const string RequestId = "owin.RequestId";
        public const string RequestScheme = "owin.RequestScheme";
        public const string RequestMethod = "owin.RequestMethod";
        public const string RequestPathBase = "owin.RequestPathBase";
        public const string RequestPath = "owin.RequestPath";
        public const string RequestQueryString = "owin.RequestQueryString";
        public const string RequestProtocol = "owin.RequestProtocol";
        public const string RequestHeaders = "owin.RequestHeaders";
        public const string RequestBody = "owin.RequestBody";
        public const string RequestUser = "owin.RequestUser";

        public const string ResponseStatusCode = "owin.ResponseStatusCode";
        public const string ResponseReasonPhrase = "owin.ResponseReasonPhrase";
        public const string ResponseProtocol = "owin.ResponseProtocol";
        public const string ResponseHeaders = "owin.ResponseHeaders";
        public const string ResponseBody = "owin.ResponseBody";

        public const string ServerUser = "server.User";

        public const string CallCancelled = "owin.CallCancelled";
    }
}
