using Dm.Auto.Testing.Core.Browsers;
using Dm.Auto.Testing.Core.Elements;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Samples.Components
{
    public class AccountActionsBlock : Component
    {
        public AccountActionsBlock(IWebElement element, IWebDriver webDriver, IBrowser browser) : base(element, webDriver, browser)
        {
        }

        private IElement LoginLink => Find.ById("LoginLink").AsElement();
        private IElement LogoutLink => Find.ById("LogoutLink").AsElement();
        private IElement RegistrationLink => Find.ById("RegisterLink").AsElement();

        public bool IsGuestActions => LoginLink.Exists && RegistrationLink.Exists && !LoginLink.Exists;

        public void BeginLogin()
        {
            Browser.PrepareForAjaxRequest();
            LoginLink.Click();
            Browser.WaitForPendingAjaxRequests();
        }

        public void BeginRegistration()
        {
            Browser.PrepareForAjaxRequest();
            LoginLink.Click();
            Browser.WaitForPendingAjaxRequests();
        }
    }
}