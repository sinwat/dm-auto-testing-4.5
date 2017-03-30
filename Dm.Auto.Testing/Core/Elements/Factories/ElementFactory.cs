using System;
using Dm.Auto.Testing.Core.Elements.Searchers;
using OpenQA.Selenium;
using Dm.Auto.Testing.Core.Browsers;

namespace Dm.Auto.Testing.Core.Elements.Factories
{
    public class ElementFactory : IElementFactory
    {
        private readonly By searchCriteria;
        private readonly ISearchable searchable;
        private readonly IBrowser Browser;

        public ElementFactory(IWebElement element, IWebDriver webDriver, ISearchable searchable, IBrowser browser)
        {
            Element = element;
            this.searchable = searchable;
            WebDriver = webDriver;
            Browser = browser;
        }

        public ElementFactory(By searchCriteria, ISearchable searchable)
        {
            this.searchCriteria = searchCriteria;
            this.searchable = searchable;
        }

        public IWebElement Element { get; }
        public IWebDriver WebDriver { get; }

        public IElement AsElement()
        {
            if (Element == null)
            {
                return new EmptyElement(searchCriteria, searchable);
            }
            return new Element(Element, WebDriver);
        }

        public TComponent AsComponent<TComponent>() where TComponent : class, IComponent
        {
            return (TComponent)Activator.CreateInstance(typeof(TComponent), Element, WebDriver, Browser);
        }
    }
}