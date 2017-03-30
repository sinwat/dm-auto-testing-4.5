using Dm.Auto.Testing.Core.Elements.BaseComponents;
using Dm.Auto.Testing.Core.Elements.Factories;

namespace Dm.Auto.Testing.Core.Extensions
{
    public static class ElementFactoryExtensions
    {
        public static ITextInputElement AsTextInput(this IElementFactory factory)
        {
            return new TextInputElement(factory.Element, factory.WebDriver);
        }

        public static ISelector AsSelector(this IElementFactory factory)
        {
            return new Selector(factory.Element, factory.WebDriver);
        }
    }
}