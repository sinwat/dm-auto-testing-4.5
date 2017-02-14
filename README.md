# dm-auto-testing-4.5

This is the testing framework for DM.am website, but it can be used anywhere else. It is based on:

- BDD via Specflow
- NUnit as a test runner / Assertion library
- StructureMap as a IoC-container

## Concepts

The main concept of a framework is recursive-tree-structure.

1. Browser - it runs JavaScript, works with Cookies, orchestrate the Pages
2. Page - a single page that is defined by its Uri, it also can run JS, and it also can find elements within.
3. Element - basic tag element. It consists of several bools, integers and string values, such as `TagName` or `Width` etc.
4. Component - pretty much like element, but can be used to find elements or components within, just like any page. Consider it as a block of elements that you might want to use from time to time.

## Extension points

First of all, you might wanna create your own `Page`s. It's pretty simple, you just need to create your class for `IPage` interface (you may use abstract `Page` that contains some implementation).

```
public class HomePage : Page
{
        public override string Uri => string.Empty;
        
        public IElement LoginLink => Get.ById("LoginLink").AsElement();
        public IComponent LoginForm => Get.ById("LoginForm").AsComponent<LoginForm>();
}
```

Now you may notice, we use a component `LoginForm`. It is also the extension we made for this example:

```
public class LoginForm : Component
{
        public LoginForm(IWebElement element, IWebDriver webDriver) : base(element, webDriver)
        {
        }

        public ITextInputElement Login => Get.ById("Login").AsTextInput();
        public ITextInputElement Password => Get.ById("Password").AsTextInput();
}
```

Okay, again new names! `ITextInputElement` is a small extension over `IElement` (which is basic single html node) that allows you to operate with text inputs of any type (like `text` or `password` etc). You can do it by getting/setting value of `Text` property.

```
HomePage homePage = new HomePage();
homePage.LoginForm.Login = "Test"; // will send keys to the input
homePage.LoginForm.Login; // will return "Test"
```

You can create your own types of controls for any situation, by extending the `IElementFactory` class:

```
public static class MyElementTypes
{
        public static IMyElement AsMyElement(this IElementFactory factory)
        {
                // factory.Element is an instance of IWebElement
                // factory.WebDriver is an instance of IWebDriver
                // which are Selenium interfaces
                return new MyElement(factory.Element, factory.WebDriver);
        }
        
        public static IMyElement[] AsMyElements(this IElementFactory[] factories)
        {
                return factories
                        .Select(f => new MyElement(f.Element, f.WebDriver))
                        .Cast<IMyElement>()
                        .ToArray();
        }
}

...

public IMyElement MyCoolElement => Find.ByCss(".cool-elements-selector").AsMyElements();

```

## Writing tests

The `Tests` namespace consists of two base classes: `ScenarioAspect` and `FeatureStepsBase`.

`ScenarioAspect` builds a configuration, creates the IoC-Container, initiates the browser pool, and saves a screenshot if the test has failed.

You will need `FeatureStepsBase` to inject an `IBrowser` instance inside your step definitions. I use it as a base class for my step definitions.

```
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
```
