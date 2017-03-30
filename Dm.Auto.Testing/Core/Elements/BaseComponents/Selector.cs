using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Dm.Auto.Testing.Core.Elements.BaseComponents
{
    public class Selector : Element, ISelector
    {
        protected readonly SelectElement SelectElement;

        public Selector(IWebElement element, IWebDriver webDriver) : base(element, webDriver)
        {
            SelectElement = new SelectElement(element);
        }

        public string Selected => SelectElement.SelectedOption.Text;

        public string[] Options()
        {
            return SelectElement.Options.Select(el => el.Text).ToArray();
        }

        public void Select(int index)
        {
            SelectElement.SelectByIndex(index);
        }

        public void Select(string text)
        {
            SelectElement.SelectByText(text);
        }

        public void SelectFirstThatStartsWith(string text)
        {
            var optionValue = Array.Find(Options(), o => o.StartsWith(text));
            Select(optionValue);
        }

        public void SelectRandom()
        {
            var index = new Random().Next(0, SelectElement.Options.Count);
            Select(index);
        }
    }
}
