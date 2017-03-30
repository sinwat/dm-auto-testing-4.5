using Dm.Auto.Testing.Core.Browsers;
using Dm.Auto.Testing.Core.Elements.Searchers;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Elements
{
    public class Component : Element, IComponent
    {
        protected readonly IBrowser Browser;
        private readonly ElementSearcher elementSearcher;

        public Component(IWebElement element, IWebDriver webDriver, IBrowser browser) : base(element, webDriver)
        {
            Browser = browser;
            elementSearcher = new ElementSearcher(element, this, webDriver, browser);
        }

        public IElementGetter Get => elementSearcher;
        public IElementFinder Find => elementSearcher;
    }
}