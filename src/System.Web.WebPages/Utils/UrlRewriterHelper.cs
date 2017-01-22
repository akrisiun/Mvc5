// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Specialized;

namespace System.Web.WebPages
{
    internal class UrlRewriterHelper
    {
        // internal for tests
        internal const string UrlWasRewrittenServerVar = "IIS_WasUrlRewritten";
        internal const string UrlRewriterEnabledServerVar = "IIS_UrlRewriteModule";

        internal const string UrlWasRequestRewrittenTrueValue = "true";
        internal const string UrlWasRequestRewrittenFalseValue = "false";

        private object _lockObject = new object();
        private bool _urlRewriterIsTurnedOnValue;
        private volatile bool _urlRewriterIsTurnedOnCalculated = false;

        private static bool WasThisRequestRewritten(HttpContextBase httpContext)
        {
            if (httpContext.Items.Contains(UrlWasRewrittenServerVar))
            {
                return Object.Equals(httpContext.Items[UrlWasRewrittenServerVar], UrlWasRequestRewrittenTrueValue);
            }
            else
            {
                HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)httpContext.GetService(typeof(HttpWorkerRequest));
                bool requestWasRewritten = (httpWorkerRequest != null
                    && httpWorkerRequest.GetServerVariableSafe<object>(UrlWasRewrittenServerVar, null) != null);

                if (requestWasRewritten)
                {
                    httpContext.Items.Add(UrlWasRewrittenServerVar, UrlWasRequestRewrittenTrueValue);
                }
                else
                {
                    httpContext.Items.Add(UrlWasRewrittenServerVar, UrlWasRequestRewrittenFalseValue);
                }

                return requestWasRewritten;
            }
        }

        private bool IsUrlRewriterTurnedOn(HttpContextBase httpContext)
        {
            // Need to do double-check locking because a single instance of this class is shared in the entire app domain (see PathHelpers)
            if (!_urlRewriterIsTurnedOnCalculated)
            {
                lock (_lockObject)
                {
                    if (!_urlRewriterIsTurnedOnCalculated)
                    {
                        HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)httpContext.GetService(typeof(HttpWorkerRequest));
                        bool urlRewriterIsEnabled = (httpWorkerRequest != null
                             && httpWorkerRequest.GetServerVariableSafe<object>(UrlRewriterEnabledServerVar, null) != null);
                        _urlRewriterIsTurnedOnValue = urlRewriterIsEnabled;
                        _urlRewriterIsTurnedOnCalculated = true;
                    }
                }
            }
            return _urlRewriterIsTurnedOnValue;
        }

        public virtual bool WasRequestRewritten(HttpContextBase httpContext)
        {
            return IsUrlRewriterTurnedOn(httpContext) && WasThisRequestRewritten(httpContext);
        }
    }

    internal static class HttpWorkerRequestSafe
    {
        public static T GetServerVariableSafe<T>(this HttpWorkerRequest wr, string key, T safeValue) where T : class
        {
            object result = null;
            try
            {
                result = wr.GetServerVariable(key);
            }
            catch {; } // ignore : Value does not fall within the expected range. 

            return (result as T) != null ? (result as T) : safeValue;
        }

    }
}
