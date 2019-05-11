// no Copyright


using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace System.Web.WebPages.Run 
{
    // src\System.Web.WebPages\WebPage.cs

    public static class WebPageRun
    {
        public static StringWriter Render(this WebPageBase page, string url, string cshtml,
            StartPage startPage = null, HttpContextBase ctx = null)
        {
            var writer = new StringWriter();

            // Create an actual HttpContext that has a request object
            // request = request ?? CreateRequest(cshtml, url).Object;
            var httpContext = ctx ?? new HttpContextWrapper(HttpContext.Current);

            var pageContext = new WebPageContext { HttpContextSet = httpContext };

            page.ExecutePageHierarchy(pageContext, writer, startPage);
            return writer;
        }

        /*
        public static Mock<HttpContextBase> CreateContext(HttpRequestBase request = null, 
                 HttpResponseBase response = null, IDictionary items = null)
        {
            items = items ?? new Hashtable();
            request = request ?? CreateTestRequest("default.cshtml", "http://localhost/default.cshtml").Object;

            if (response == null)
            {
                var mockResponse = new Mock<HttpResponseBase>();
                mockResponse.Setup(r => r.Cookies).Returns(new HttpCookieCollection());
                response = mockResponse.Object;
            }

            var httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(c => c.Items).Returns(items);
            httpContext.SetupGet(c => c.Request).Returns(request);
            httpContext.SetupGet(c => c.Response).Returns(response);
            return httpContext;
        }

        public static Mock<HttpRequestBase> CreateRequest(string filename, string url)
        {
            var mockRequest = new Mock<HttpRequestBase> { CallBase = true };
            mockRequest.SetupGet(r => r.Path).Returns(filename);
            mockRequest.SetupGet(r => r.RawUrl).Returns(url);
            mockRequest.SetupGet(r => r.IsLocal).Returns(false);
            mockRequest.SetupGet(r => r.QueryString).Returns(new NameValueCollection());
            mockRequest.SetupGet(r => r.Browser.IsMobileDevice).Returns(false);
            mockRequest.SetupGet(r => r.Cookies).Returns(new HttpCookieCollection());
            mockRequest.SetupGet(r => r.UserAgent).Returns(String.Empty);

            return mockRequest;
        }
        */

        public static WebPage PrepareRenderPage<T>(Action<WebPage> pageExecuteAction, string pagePath = "~/index.cshtml") where T : WebPage
        {
            WebPage page = Activator.CreateInstance<T>();
            
            var pageObj = CreatePage(page, pageExecuteAction, pagePath);
            return pageObj as T;
        }

        public static WebPage CreatePage(WebPage page, Action<WebPage> pageExecuteAction, string pagePath = "~/index.cshtml")
        {
            /* var page = new WebPage()
            {
                VirtualPath = pagePath,
                ExecuteAction = p => { pageExecuteAction(p); }
            }; */
            page.VirtualPath = pagePath;
            // page.ExecuteAction = p => { pageExecuteAction(p); }

            page.VirtualPathFactory = new HashVirtualPathFactory(page);
            page.DisplayModeProvider = new DisplayModeProvider();
            return page;
        }


    }

    public class RunWebPage : WebPage // <object>
    {
        public Action<WebPage> ExecuteAction { get; set; }

        // internal 
        protected override string GetDirectory(string virtualPath)
        {
            // base.GetDirectory()
            return RunStartPage.Directory(virtualPath);
        }

        public override void Execute()
        {
            ExecuteAction(this);
        }
    }

    public class RunStartPage : StartPage
    {
        public Action<StartPage> ExecuteAction { get; set; }

        internal static string Directory(string virtualPath)
        {
            var dir = Path.GetDirectoryName(virtualPath);
            if (dir == "~")
            {
                return null;
            }
            return dir;
        }

        // internal 
        protected override string GetDirectory(string virtualPath)
        {
            return Directory(virtualPath);
        }

        public override void Execute()
        {
            ExecuteAction(this);
        }
    }

    public class HashVirtualPathFactory : IVirtualPathFactory
    {
        private IDictionary<string, object> _pages;

        public HashVirtualPathFactory(params WebPageExecutingBase[] pages)
        {
            _pages = pages.ToDictionary(p => p.VirtualPath, p => (object)p, StringComparer.OrdinalIgnoreCase);
        }

        public bool Exists(string virtualPath)
        {
            return _pages.ContainsKey(virtualPath);
        }

        public object CreateInstance(string virtualPath)
        {
            object value;
            if (_pages.TryGetValue(virtualPath, out value))
            {
                return value;
            }
            return null;
        }
    }

}