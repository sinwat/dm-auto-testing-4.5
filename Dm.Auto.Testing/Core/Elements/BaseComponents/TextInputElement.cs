﻿using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Elements.BaseComponents
{
    public class TextInputElement : Element, ITextInputElement
    {
        public TextInputElement(IWebElement element, IWebDriver webDriver) : base(element, webDriver)
        {
        }

        public string Value
        {
            get { return GetAttribute("value"); }
            set
            {
                Click();
                WebElement.Clear();
                WebElement.SendKeys(value);
            }
        }

        public void Clear()
        {
            WebElement.Clear();
        }

        public void Submit()
        {
            WebElement.Submit();
        }
    }
}