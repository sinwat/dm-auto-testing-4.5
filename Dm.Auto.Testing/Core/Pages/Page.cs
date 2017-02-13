using Dm.Auto.Testing.Core.Browsers;
using Dm.Auto.Testing.Core.Elements.Searchers;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Pages
{
    public abstract class Page : IPage
    {
        private ElementSearcher elementSearcher;
        protected IWebDriver WebDriver { get; private set; }
        protected IBrowser Browser { get; private set; }

        public abstract string Uri { get; }
        public string Url => WebDriver.Url;
        public bool IsError => false;
        public bool IsLoaded => ((IJavaScriptExecutor)WebDriver).ExecuteScript("return document.readyState;").Equals("complete");
        public void Initialize(IWebDriver webDriver, IBrowser browser)
        {
            WebDriver = webDriver;
            Browser = browser;
            elementSearcher = new ElementSearcher(webDriver, this, webDriver);
        }

        public IElementGetter Get => elementSearcher;
        public IElementFinder Find => elementSearcher;
    }
}