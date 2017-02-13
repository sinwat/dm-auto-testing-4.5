using Dm.Auto.Testing.Core.Browsers;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Drivers
{
    public interface ICompositeWebDriverFactory
    {
        IWebDriver Create(BrowserType browserType);
    }
}