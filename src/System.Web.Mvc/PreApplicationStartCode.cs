// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel;
using System.Web.WebPages.Scope;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace System.Web.Mvc
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PreApplicationStartCode
    {
        private static bool _startWasCalled;

        static PreApplicationStartCode()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            Exception loadError = null;
            try
            {
                if (File.Exists(Path.Combine(baseDir, @"bin\Microsoft.Web.Infrastructure.dll")))
                    Assembly.LoadFrom(Path.Combine(baseDir, @"bin\Microsoft.Web.Infrastructure.dll"));
            }
            catch (Exception ex) { loadError = ex; }
            if (loadError != null && Debugger.IsAttached)
                Debugger.Log(0, "Mvc:: WebPages.PreApplicationStartCode error", loadError.Message);
        }

        public static void Start()
        {
            // Guard against multiple calls. All Start calls are made on same thread, so no lock needed here
            if (_startWasCalled)
            {
                return;
            }
            _startWasCalled = true;

            WebPages.Razor.PreApplicationStartCode.Start();

            WebPages.PreApplicationStartCode.Start();

            ViewContext.GlobalScopeThunk = () => ScopeStorage.CurrentScope;
        }
    }
}
