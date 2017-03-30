using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using Dm.Auto.Testing.Core.Browsers.Ajax;
using Dm.Auto.Testing.Core.Pages;
using Dm.Auto.Testing.Core.Utils;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Browsers
{
    public class Browser : IBrowser
    {
        private readonly string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        private IPage currentPage;

        private readonly JavaScriptSerializer serializer;

        public Browser(
            IWebDriver webDriver
            )
        {
            WebDriver = webDriver;
            serializer = new JavaScriptSerializer();
        }
        public IWebDriver WebDriver { get; }

        public TPage GetCurrent<TPage>() where TPage : class, IPage
        {
            if (currentPage == null)
            {
                throw new CurrentPageException<TPage>();
            }
            var castedCurrentPage = currentPage as TPage;
            if (castedCurrentPage == null)
            {
                throw new CurrentPageException<TPage>(currentPage.Url);
            }
            return castedCurrentPage;
        }

        public TPage GetCurrentUnsafe<TPage>() where TPage : class, IPage, new()
        {
            var page = new TPage();
            page.Initialize(WebDriver, this);

            currentPage = page;
            return page;
        }

        public TPage GoTo<TPage>() where TPage : class, IPage, new()
        {
            var page = new TPage();
            page.Initialize(WebDriver, this);
            WebDriver.Url = $"{baseUrl}/{page.Uri}";

            Wait.For(() =>
            {
                if (page.IsError)
                {
                    throw new PageLoadException<TPage>(page);
                }
                return page.IsLoaded;
            }, 60000);
            currentPage = page;
            return page;
        }

        public TPage WaitForSubmit<TPage>() where TPage : class, IPage, new()
        {
            var jsExecutor = (IJavaScriptExecutor)WebDriver;
            jsExecutor.ExecuteScript("window.__seleniumSubmitFlag__ = true;");
            Wait.UntilChanged("true", () => jsExecutor.ExecuteScript("window.__seleniumSubmitFlag__"));
            Wait.For(2000);

            return GetCurrentUnsafe<TPage>();
        }
        
        public void PrepareForAjaxRequest()
        {
            WaitForAjaxRequests();

            const string script = "if (typeof jQuery == 'undefined') return; \n" +
                                  "if (typeof ajaxRequests != 'undefined') { \n" +
                                  "    ajaxRequests = []; \n" +
                                  "    return; \n" +
                                  "} \n" +
                                  "ajaxRequests = []; \n" +
                                  "$(document).ajaxSend(function(event, jqxhr, settings) { \n" +
                                  "    var ajaxRequest = { \n" +
                                  "                          async: settings.async, \n" +
                                  "                          type: settings.type, \n" +
                                  "                          url: settings.url, \n" +
                                  "                          state: 'Pending', \n" +
                                  "                          postData: settings.type == 'POST' ? settings.data : '' \n" +
                                  "                      }; \n" +
                                  "    ajaxRequests.push(ajaxRequest); \n" +
                                  "}); \n" +
                                  "$(document).ajaxSuccess(function(event, jqxhr, settings) { \n" +
                                  "    for (var i in ajaxRequests) { \n" +
                                  "        if (ajaxRequests[i].url == settings.url) { \n" +
                                  "            ajaxRequests[i].state = 'Succeeded'; \n" +
                                  "            ajaxRequests[i].httpStatusCode = jqxhr.status; \n" +
                                  "        } \n" +
                                  "    } \n" +
                                  "}); \n" +
                                  "$(document).ajaxError(function(event, jqxhr, settings) { \n" +
                                  "    for (var i in ajaxRequests) { \n" +
                                  "        if (ajaxRequests[i].url == settings.url) { \n" +
                                  "            ajaxRequests[i].state = 'Failed'; \n" +
                                  "            ajaxRequests[i].httpStatusCode = jqxhr.status; \n" +
                                  "        } \n" +
                                  "    } \n" +
                                  "});";
            ExecuteScript(script);
        }

        public void WaitForPendingAjaxRequests()
        {
            WaitForAjaxRequests();
            var requests = ReadRequestsState();

            if (requests == string.Empty)
            {
                return;
            }

            var ajaxRequests = serializer.Deserialize<AjaxRequest[]>(requests);

            if (ajaxRequests.Length == 0)
            {
                return;
            }

            var failedCount = ajaxRequests.Count(x => x.State == AjaxRequestState.Failed);
            if (failedCount > 0)
            {
                throw new AjaxRequestFailedException(ajaxRequests.First(x => x.State == AjaxRequestState.Failed));
            }
        }

        private string ReadRequestsState()
        {
            const string script = "if (typeof jQuery == 'undefined') return ''; \n" +
                                  "return (typeof ajaxRequests == 'undefined') ? '' : JSON.stringify(ajaxRequests)";
            return ExecuteScript(script);
        }

        private void WaitForAjaxRequests()
        {
            const string script = "return (typeof jQuery == 'undefined' || jQuery.active == 0).toString()";
            Wait.For(() => bool.Parse(ExecuteScript(script)), () =>
                $"Unable to wait for ajax request result. State: {ReadRequestsState()}", 80000);
        }

        public string ExecuteScript(string script)
        {
            return (string)((IJavaScriptExecutor) WebDriver).ExecuteScript(script);
        }

        private static int screenshotNumber;
        private static readonly ImageFormat ImageFormat = ImageFormat.Gif;

        public void SaveScreenshot()
        {
            var screenshot = ((ITakesScreenshot)WebDriver).GetScreenshot();
            using (var memoryStream = new MemoryStream(screenshot.AsByteArray))
            {
                var image = Image.FromStream(memoryStream);
                var number = Interlocked.Increment(ref screenshotNumber);
                var screenshotPath = $"{ConfigurationManager.AppSettings["ScreenshotsDir"]}/Screenshots/{number}.{ImageFormat}";
                image.Save(screenshotPath, ImageFormat);
            }
        }

        public void Dispose()
        {
            WebDriver.Dispose();
        }
    }
}