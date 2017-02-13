using Dm.Auto.Testing.Core.Elements;
using Dm.Auto.Testing.Core.Elements.BaseComponents;
using Dm.Auto.Testing.Core.Extensions;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Tests.PageDefinitions
{
    public class LoginForm : Component
    {
        public LoginForm(IWebElement element, IWebDriver webDriver) : base(element, webDriver)
        {
        }

        public ITextInputElement Login => Get.ById("Login").AsTextInput();
        public ITextInputElement Password => Get.ById("Password").AsTextInput();
    }
}