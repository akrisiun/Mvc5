﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc.Properties;
using System.Web.Mvc.Routing;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc.Html
{
    public static class ChildActionExtensions
    {
        // Action

        [Obsolete("Child actions should be obtained asynchronously, use ActionAsync instead.")]
        public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName)
        {
            return Action(htmlHelper, actionName, null /* controllerName */, null /* routeValues */);
        }

        [Obsolete("Child actions should be obtained asynchronously, use ActionAsync instead.")]
        public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, object routeValues)
        {
            return Action(htmlHelper, actionName, null /* controllerName */, TypeHelper.ObjectToDictionary(routeValues));
        }

        [Obsolete("Child actions should be obtained asynchronously, use ActionAsync instead.")]
        public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues)
        {
            return Action(htmlHelper, actionName, null /* controllerName */, routeValues);
        }

        [Obsolete("Child actions should be obtained asynchronously, use ActionAsync instead.")]
        public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            return Action(htmlHelper, actionName, controllerName, null /* routeValues */);
        }

        [Obsolete("Child actions should be obtained asynchronously, use ActionAsync instead.")]
        public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            return Action(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues));
        }

        [Obsolete("Child actions should be obtained asynchronously, use ActionAsync instead.")]
        public static MvcHtmlString Action(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            using (var writer = new StringBlockWriter(CultureInfo.CurrentCulture))
            {
                ActionHelper(htmlHelper, actionName, controllerName, routeValues, writer);
                return MvcHtmlString.Create(writer.ToString());
            }
        }

        public static Task<MvcHtmlString> ActionAsync(this HtmlHelper htmlHelper, string actionName)
        {
            return ActionAsync(htmlHelper, actionName, null /* controllerName */, null /* routeValues */);
        }

        public static Task<MvcHtmlString> ActionAsync(this HtmlHelper htmlHelper, string actionName, object routeValues)
        {
            return ActionAsync(htmlHelper, actionName, null /* controllerName */, TypeHelper.ObjectToDictionary(routeValues));
        }

        public static Task<MvcHtmlString> ActionAsync(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues)
        {
            return ActionAsync(htmlHelper, actionName, null /* controllerName */, routeValues);
        }

        public static Task<MvcHtmlString> ActionAsync(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            return ActionAsync(htmlHelper, actionName, controllerName, null /* routeValues */);
        }

        public static Task<MvcHtmlString> ActionAsync(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            return ActionAsync(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues));
        }

        public static async Task<MvcHtmlString> ActionAsync(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            using (var writer = new StringBlockWriter(CultureInfo.CurrentCulture))
            {
                await ActionHelperAsync(htmlHelper, actionName, controllerName, routeValues, writer).ConfigureAwait(false);
                return MvcHtmlString.Create(writer.ToString());
            }
        }

        // RenderAction
        [Obsolete("Child actions should be rendered asynchronously, use RenderActionAsync instead.")]
        public static void RenderAction(this HtmlHelper htmlHelper, string actionName)
        {
            RenderAction(htmlHelper, actionName, null /* controllerName */, null /* routeValues */);
        }

        [Obsolete("Child actions should be rendered asynchronously, use RenderActionAsync instead.")]
        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, object routeValues)
        {
            RenderAction(htmlHelper, actionName, null /* controllerName */, TypeHelper.ObjectToDictionary(routeValues));
        }

        [Obsolete("Child actions should be rendered asynchronously, use RenderActionAsync instead.")]
        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues)
        {
            RenderAction(htmlHelper, actionName, null /* controllerName */, routeValues);
        }

        [Obsolete("Child actions should be rendered asynchronously, use RenderActionAsync instead.")]
        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            RenderAction(htmlHelper, actionName, controllerName, null /* routeValues */);
        }

        [Obsolete("Child actions should be rendered asynchronously, use RenderActionAsync instead.")]
        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            RenderAction(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues));
        }

        [Obsolete("Child actions should be rendered asynchronously, use RenderActionAsync instead.")]
        public static void RenderAction(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            ActionHelper(htmlHelper, actionName, controllerName, routeValues, htmlHelper.ViewContext.Writer);
        }

        public static Task RenderActionAsync(this HtmlHelper htmlHelper, string actionName)
        {
            return RenderActionAsync(htmlHelper, actionName, null /* controllerName */, null /* routeValues */);
        }

        public static Task RenderActionAsync(this HtmlHelper htmlHelper, string actionName, object routeValues)
        {
            return RenderActionAsync(htmlHelper, actionName, null /* controllerName */, TypeHelper.ObjectToDictionary(routeValues));
        }

        public static Task RenderActionAsync(this HtmlHelper htmlHelper, string actionName, RouteValueDictionary routeValues)
        {
            return RenderActionAsync(htmlHelper, actionName, null /* controllerName */, routeValues);
        }

        public static Task RenderActionAsync(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            return RenderActionAsync(htmlHelper, actionName, controllerName, null /* routeValues */);
        }

        public static Task RenderActionAsync(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            return RenderActionAsync(htmlHelper, actionName, controllerName, TypeHelper.ObjectToDictionary(routeValues));
        }

        public static async Task RenderActionAsync(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            await ActionHelperAsync(htmlHelper, actionName, controllerName, routeValues, htmlHelper.ViewContext.Writer).ConfigureAwait(false);
        }

        // Helpers
        private static IHttpHandler PrepareActionHelper(HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, out HttpContextBase httpContext)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }
            if (String.IsNullOrEmpty(actionName))
            {
                throw new ArgumentException(MvcResources.Common_NullOrEmpty, "actionName");
            }

            RouteValueDictionary additionalRouteValues = routeValues;
            routeValues = MergeDictionaries(routeValues, htmlHelper.ViewContext.RouteData.Values);

            routeValues["action"] = actionName;
            if (!String.IsNullOrEmpty(controllerName))
            {
                routeValues["controller"] = controllerName;
            }

            bool usingAreas;
            VirtualPathData vpd = htmlHelper.RouteCollection.GetVirtualPathForArea(htmlHelper.ViewContext.RequestContext, null /* name */, routeValues, out usingAreas);
            if (vpd == null)
            {
                throw new InvalidOperationException(MvcResources.Common_NoRouteMatched);
            }

            if (usingAreas)
            {
                routeValues.Remove("area");
                if (additionalRouteValues != null)
                {
                    additionalRouteValues.Remove("area");
                }
            }

            if (additionalRouteValues != null)
            {
                routeValues[ChildActionValueProvider.ChildActionValuesKey] = new DictionaryValueProvider<object>(additionalRouteValues, CultureInfo.InvariantCulture);
            }

            RouteData routeData = CreateRouteData(vpd.Route, routeValues, vpd.DataTokens, htmlHelper.ViewContext);
            httpContext = htmlHelper.ViewContext.HttpContext;
            RequestContext requestContext = new RequestContext(httpContext, routeData);
            IHttpHandler handler = new ChildActionMvcHandler(requestContext);
            return handler;
        }

        private static void ExecuteHandler(TextWriter textWriter, HttpContextBase httpContext, IHttpHandler handler)
        {
            httpContext.Server.Execute(HttpHandlerUtil.WrapForServerExecute(handler), textWriter, true /* preserveForm */);
        }

        internal static void ActionHelper(HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, TextWriter textWriter)
        {
            HttpContextBase httpContext;
            var handler = PrepareActionHelper(htmlHelper, actionName, controllerName, routeValues, out httpContext);
            ExecuteHandler(textWriter, httpContext, handler);
        }

        internal static async Task ActionHelperAsync(HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, TextWriter textWriter)
        {
            HttpContextBase httpContext;
            var handler = PrepareActionHelper(htmlHelper, actionName, controllerName, routeValues, out httpContext);
            var asyncHandler = handler as IHttpAsyncHandler;
            if (asyncHandler == null)
            {
                ExecuteHandler(textWriter, httpContext, handler);
                return;
            }
            if (_HttpResponseSwitchWriter == null)
            {
                throw new TypeLoadException("Failed to access HttpResponse internals.");
            }
            var innerContext = httpContext.ApplicationInstance.Context;
            var response = innerContext.Response;
            TextWriter prevWriter = null;
            bool handlerSet = false;
            try
            {
                prevWriter = _HttpResponseSwitchWriter(response, textWriter);
                if (_HttpContextSetCurrentHandler != null && _HttpContextRestoreCurrentHandler != null)
                {
                    try
                    {
                    }
                    finally
                    {
                        _HttpContextSetCurrentHandler(innerContext, asyncHandler);
                        handlerSet = true;
                    }
                }
                await Task.Factory.FromAsync(asyncHandler.BeginProcessRequest, asyncHandler.EndProcessRequest, innerContext, null).ConfigureAwait(false);
            }
            finally
            {
                if (handlerSet)
                {
                    _HttpContextRestoreCurrentHandler(innerContext);
                }
                if (prevWriter != null)
                {
                    _HttpResponseSwitchWriter(response, prevWriter);
                }
            }
        }

        private static RouteData CreateRouteData(RouteBase route, RouteValueDictionary routeValues, RouteValueDictionary dataTokens, ViewContext parentViewContext)
        {
            RouteData routeData = new RouteData();

            foreach (KeyValuePair<string, object> kvp in routeValues)
            {
                routeData.Values.Add(kvp.Key, kvp.Value);
            }

            foreach (KeyValuePair<string, object> kvp in dataTokens)
            {
                routeData.DataTokens.Add(kvp.Key, kvp.Value);
            }

            routeData.Route = route;
            routeData.DataTokens[ControllerContext.ParentActionViewContextToken] = parentViewContext;

            // It's possible that the outgoing route is a direct route - in which case it's not possible to reach using
            // the action name and controller name. We need to check for that case to determine if we need to create a 
            // 'direct route' routedata to reach it.
            if (route.IsDirectRoute())
            {
                // Codeplex-2136 - ControllerContext.IsChildAction returns false inside Controller.Initialize()
                //
                // We're constructing a 'temp' route data to wrap the route data for the match we're invoking via
                // an attribute route. The ControllerContext will look at datatokens to see if it's being invoked
                // as a child action. 
                //
                // By sticking the view context on both route data ControllerContext will do the right thing
                // at all parts of the pipeline.
                var directRouteData = RouteCollectionRoute.CreateDirectRouteMatch(route, new List<RouteData>() { routeData });
                directRouteData.DataTokens[ControllerContext.ParentActionViewContextToken] = parentViewContext;
                return directRouteData;
            }
            else
            {
                return routeData;
            }
        }

        private static RouteValueDictionary MergeDictionaries(params RouteValueDictionary[] dictionaries)
        {
            // Merge existing route values with the user provided values
            var result = new RouteValueDictionary();

            foreach (RouteValueDictionary dictionary in dictionaries.Where(d => d != null))
            {
                foreach (KeyValuePair<string, object> kvp in dictionary)
                {
                    if (!result.ContainsKey(kvp.Key))
                    {
                        result.Add(kvp.Key, kvp.Value);
                    }
                }
            }

            return result;
        }

        internal class ChildActionMvcHandler : MvcHandler
        {
            public ChildActionMvcHandler(RequestContext context)
                : base(context)
            {
            }

            protected // internal 
                override void AddVersionHeader(HttpContextBase httpContext)
            {
                // No version header for child actions
            }
        }

#pragma warning disable 0436
        private static readonly Func<HttpResponse, TextWriter, TextWriter> _HttpResponseSwitchWriter = ReflectionHelpers.CreateMethodCallerWithResult<HttpResponse, TextWriter, TextWriter>("SwitchWriter");

        private static readonly Action<HttpContext, IHttpHandler> _HttpContextSetCurrentHandler = ReflectionHelpers.CreateMethodCaller<HttpContext, IHttpHandler>("SetCurrentHandler");

        private static readonly Action<HttpContext> _HttpContextRestoreCurrentHandler = ReflectionHelpers.CreateMethodCaller<HttpContext>("RestoreCurrentHandler");
#pragma warning restore 0436
    }
}
