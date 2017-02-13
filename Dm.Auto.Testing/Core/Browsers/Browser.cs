using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using Dm.Auto.Testing.Core.Pages;
using Dm.Auto.Testing.Core.Utils;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Browsers
{
    public class Browser : IBrowser
    {
        private readonly string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
        private IPage currentPage;

        public Browser(
            IWebDriver webDriver
            )
        {
            WebDriver = webDriver;
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

        public TPage GoTo<TPage>(string queryParams = null) where TPage : IPage, new()
        {
            var page = new TPage();
            page.Initialize(WebDriver, this);
            WebDriver.Url = $"{baseUrl}/{page.Uri}/{queryParams}";

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