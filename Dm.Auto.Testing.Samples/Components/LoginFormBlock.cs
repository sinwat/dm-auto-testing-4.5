using Dm.Auto.Testing.Core.Browsers;
using Dm.Auto.Testing.Core.Elements;
using Dm.Auto.Testing.Core.Extensions;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Samples.Components
{
    public class LoginFormBlock : Component
    {
        public LoginFormBlock(IWebElement element, IWebDriver webDriver, IBrowser browser) : base(element, webDriver, browser)
        {
        }

        private IElement LoginInput => Get.ById("Login").AsTextInput();
        private IElement PasswordInput => Get.ById("Password").AsTextInput();

        public void EnterCredentials(string login, string password)
        {
            
        }
    }
}