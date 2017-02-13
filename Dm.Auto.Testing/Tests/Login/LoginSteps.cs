using Dm.Auto.Testing.Tests.PageDefinitions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Dm.Auto.Testing.Tests.Login
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
    }
}
