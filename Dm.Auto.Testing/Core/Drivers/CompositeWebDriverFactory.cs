using Dm.Auto.Testing.Core.Browsers;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Drivers
{
    public class CompositeWebDriverFactory : ICompositeWebDriverFactory
    {
        private readonly IWebDriverFactory[] webDriverFactories;

        public CompositeWebDriverFactory(
            IWebDriverFactory[] webDriverFactories
        )
        {
            this.webDriverFactories = webDriverFactories;
        }

        public IWebDriver Create(BrowserType browserType)
        {
            foreach (var factory in webDriverFactories)
            {
                if (factory.CanCreate(browserType))
                {
                    return factory.Create();
                }
            }
            throw new WebDriverFactoryException(browserType);
        }
    }
}