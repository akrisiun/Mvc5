// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;

namespace System.Web.WebPages
{
    public class WebPagesModule : WebPageHttpModule
    {
        public static bool IsDebug { get; set; }

        public void InitFinished(HttpApplication application)
        {
            WebPageHttpModule.AppStartExecuteCompleted = true;
        }
        public override void Init(HttpApplication application)
        {
            if (IsDebug && Debugger.IsAttached)
                Debugger.Break();

            base.Init(application);
        }
    }

    // internal 
    public class WebPageHttpModule : IHttpModule
    {
#pragma warning disable 649 
        internal static EventHandler Initialize;
        internal static EventHandler ApplicationStart;
        internal static EventHandler BeginRequest;
        internal static EventHandler EndRequest;
#pragma warning restore 649 
        private static bool _appStartExecuted = false;
        private static readonly object _appStartExecutedLock = new object();
        private static readonly object _hasBeenRegisteredKey = new object();

        internal static bool AppStartExecuteCompleted { get; set; }

        public void Dispose()
        {
        }

        public virtual void Init(HttpApplication application)
        {
            if (application.Context.Items[_hasBeenRegisteredKey] != null)
            {
                // registration for this module has already run for this HttpApplication instance
                return;
            }

            application.Context.Items[_hasBeenRegisteredKey] = true;

            InitApplication(application);
        }

        internal static void InitApplication(HttpApplication application)
        {
            // We need to run StartApplication first, so that any exception thrown during execution of the StartPage gets
            // recorded on StartPage.Exception
            StartApplication(application);
            InitializeApplication(application);
        }

        internal static void InitializeApplication(HttpApplication application)
        {
            InitializeApplication(application, OnApplicationPostResolveRequestCache, Initialize);
        }

        internal static void InitializeApplication(HttpApplication application, EventHandler onApplicationPostResolveRequestCache, EventHandler initialize)
        {
            if (initialize != null)
            {
                initialize(application, EventArgs.Empty);
            }
            application.PostResolveRequestCache += onApplicationPostResolveRequestCache;
            if (ApplicationStartPage.Exception != null || BeginRequest != null)
            {
                application.BeginRequest += OnBeginRequest;
            }

            application.EndRequest += OnEndRequest;
        }

        internal static void StartApplication(HttpApplication application)
        {
            StartApplication(application, ApplicationStartPage.ExecuteStartPage, ApplicationStart);
        }

        internal static void StartApplication(HttpApplication application, Action<HttpApplication> executeStartPage, EventHandler applicationStart)
        {
            // Application start events should happen only once per application life time.
            lock (_appStartExecutedLock)
            {
                if (!_appStartExecuted)
                {
                    _appStartExecuted = true;

                    executeStartPage(application);
                    AppStartExecuteCompleted = true;
                    if (applicationStart != null)
                    {
                        applicationStart(application, EventArgs.Empty);
                    }
                }
            }
        }

        public virtual void DoApplicationPostResolveRequestCache(HttpApplication app)
        {
            OnApplicationPostResolveRequestCache(app ?? HttpContext.Current.ApplicationInstance, EventArgs.Empty);
        }

        public virtual void DoBeginRequest(HttpApplication app)
        {
            AppStartExecuteCompleted = true;
            OnBeginRequest(app ?? HttpContext.Current.ApplicationInstance, EventArgs.Empty);
        }

        internal static void OnApplicationPostResolveRequestCache(object sender, EventArgs e)
        {
            HttpContextBase context = new HttpContextWrapper(((HttpApplication)sender).Context);
            new WebPageRoute().DoPostResolveRequestCache(context);
        }

        internal static void OnBeginRequest(object sender, EventArgs e)
        {
            if (ApplicationStartPage.Exception != null)
            {
                // Throw it as a HttpException so as to
                // display the original stack trace information.

                ApplicationStartPageEx = ApplicationStartPage.Exception;
                // throw new HttpException(null, ApplicationStartPage.Exception);
            }
            if (BeginRequest != null)
            {
                BeginRequest(sender, e);
            }
        }

        public static Exception ApplicationStartPageEx { get; private set; }

        internal static void OnEndRequest(object sender, EventArgs e)
        {
            if (ApplicationStartPageEx != null && ((int?)HttpContext.Current?.Response.StatusCode == 200))
            {
                HttpContext.Current?.Response.Write($"Startup error {ApplicationStartPageEx}");
                ApplicationStartPageEx = null;
            }

            if (EndRequest != null)
            {
                EndRequest(sender, e);
            }

            var app = (HttpApplication)sender;
            RequestResourceTracker.DisposeResources(new HttpContextWrapper(app.Context));
        }
    }
}
