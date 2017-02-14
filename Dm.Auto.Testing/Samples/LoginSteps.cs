using Dm.Auto.Testing.Core.Utils;
using Dm.Auto.Testing.Testing;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Dm.Auto.Testing.Samples
{
    [Binding]
    public sealed class LoginSteps : FeatureStepsBase
    {
        [Given("I am on home page")]
        public void GivenIAmOnHomePage()
        {
            Browser.GoTo<HomePage>();
        }

        [When("I click login button")]
        public void WhenIClickLoginLink()
        {
            Browser.GetCurrent<HomePage>().LoginLink.Click();
        }

        [Then("the login form should appear on the screen")]
        public void ThenTheLoginFormShouldAppearOnTheScreen()
        {
            Assert.IsTrue(Browser.GetCurrent<HomePage>().LoginForm.Visible);
        }

        [When("I enter login data")]
        public void WhenIEnterLoginData(Table data)
        {
            var loginService = AppendContainerInstance<LoginService>();
            loginService.EnterLoginData(data.Rows[0]["login"], data.Rows[0]["password"]);
        }

        [Then("nothing happens")]
        public void ThenNothingHappens()
        {
            Wait.For(10000);
            Assert.IsTrue(Browser.GetCurrent<HomePage>().LoginForm.Visible);
        }
    }
}