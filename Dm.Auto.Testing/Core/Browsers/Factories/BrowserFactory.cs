using Dm.Auto.Testing.Core.Drivers;

namespace Dm.Auto.Testing.Core.Browsers.Factories
{
    public class BrowserFactory : IBrowserFactory
    {
        private readonly ICompositeWebDriverFactory webDriverFactory;

        public BrowserFactory(
            ICompositeWebDriverFactory webDriverFactory
        )
        {
            this.webDriverFactory = webDriverFactory;
        }

        public IBrowser Create(BrowserType browserType)
        {
            var webDriver = webDriverFactory.Create(browserType);
            return new Browser(webDriver);
        }
    }
}