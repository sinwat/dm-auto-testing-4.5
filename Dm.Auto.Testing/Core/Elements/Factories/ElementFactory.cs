using System;
using Dm.Auto.Testing.Core.Elements.Searchers;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Elements.Factories
{
    public class ElementFactory : IElementFactory
    {
        private readonly By searchCriteria;
        private readonly ISearchable searchable;

        public ElementFactory(IWebElement element, IWebDriver webDriver, ISearchable searchable)
        {
            Element = element;
            this.searchable = searchable;
            WebDriver = webDriver;
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

        public TComponent AsComponent<TComponent>() where TComponent : IComponent
        {
            throw new Exception();
        }
    }
}