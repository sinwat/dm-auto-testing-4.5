using Dm.Auto.Testing.Core.Elements;
using Dm.Auto.Testing.Core.Pages;

namespace Dm.Auto.Testing.Tests.PageDefinitions
{
    public class HomePage : Page
    {
        public IElement LoginLink => Get.ById("LoginLink").AsElement();

        public IComponent LoginForm => Get.ById("LoginForm").AsComponent<LoginForm>();
        public override string Uri => string.Empty;
    }
}