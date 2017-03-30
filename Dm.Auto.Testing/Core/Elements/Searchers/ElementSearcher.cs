using System.Linq;
using Dm.Auto.Testing.Core.Elements.Factories;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Elements.Searchers
{
    public class ElementSearcher : IElementGetter, IElementFinder
    {
        private readonly ISearchContext searchContext;
        private readonly ISearchable searchable;
        private readonly IWebDriver webDriver;

        public ElementSearcher(
            ISearchContext searchContext,
            ISearchable searchable,
            IWebDriver webDriver
            )
        {
            this.searchContext = searchContext;
            this.searchable = searchable;
            this.webDriver = webDriver;
        }

        private IElementFactory GetElement(By searchCriteria)
        {
            var element = searchContext.FindElement(searchCriteria);
            if (element == null)
            {
                throw new ElementNotFoundException(searchCriteria, searchable);
            }
            return new ElementFactory(element, webDriver, searchable);
        }

        private IElementFactory FindElement(By searchCriteria)
        {
            var element = searchContext.FindElement(searchCriteria);
            if (element == null)
            {
                return new ElementFactory(searchCriteria, searchable);
            }
            return new ElementFactory(element, webDriver, searchable);
        }

        private IElementFactory[] FindElements(By searchCriteria)
        {
            return searchContext.FindElements(searchCriteria)
                .Select(e => new ElementFactory(e, webDriver, searchable))
                .Cast<IElementFactory>()
                .ToArray();
        }
        
        IElementFactory IElementGetter.ById(string id)
        {
            return GetElement(By.Id(id));
        }

        IElementFactory IElementGetter.ByName(string name)
        {
            return GetElement(By.Name(name));
        }

        IElementFactory IElementGetter.ByClassName(string className)
        {
            return GetElement(By.ClassName(className));
        }

        IElementFactory IElementGetter.ByCss(string css)
        {
            return GetElement(By.CssSelector(css));
        }

        IElementFactory IElementGetter.ByXPath(string xpath)
        {
            return GetElement(By.XPath(xpath));
        }

        public IElementFactory ByContent(string content)
        {
            var xpath = $"//*[normalize-space(.)='{content}' or @value='{content}']";
            return GetElement(By.XPath(xpath));
        }

        IElementFactory IElementFinder.ById(string id)
        {
            return FindElement(By.Id(id));
        }

        IElementFactory[] IElementFinder.ByName(string name)
        {
            return FindElements(By.Name(name));
        }

        IElementFactory[] IElementFinder.ByCss(string css)
        {
            return FindElements(By.CssSelector(css));
        }

        IElementFactory[] IElementFinder.ByXPath(string xpath)
        {
            return FindElements(By.XPath(xpath));
        }

        IElementFactory[] IElementFinder.ByClassName(string className)
        {
            return FindElements(By.ClassName(className));
        }
    }
}