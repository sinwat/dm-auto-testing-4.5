using Dm.Auto.Testing.Core.Browsers;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Drivers
{
    public interface IWebDriverFactory
    {
        bool CanCreate(BrowserType browserType);
        IWebDriver Create();
    }
}