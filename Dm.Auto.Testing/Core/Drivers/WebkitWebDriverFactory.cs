using System.Configuration;
using Dm.Auto.Testing.Core.Browsers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Dm.Auto.Testing.Core.Drivers
{
    public class WebkitWebDriverFactory : IWebDriverFactory
    {
        public bool CanCreate(BrowserType browserType)
        {
            return browserType == BrowserType.Webkit;
        }

        public IWebDriver Create()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService(@"..\..\Drivers");
            return new ChromeDriver(chromeDriverService, new ChromeOptions { BinaryLocation = ConfigurationManager.AppSettings["ChromeBinaryPath"] });
        }
    }
}