using Dm.Auto.Testing.Core.Elements;
using Dm.Auto.Testing.Core.Pages;

namespace Dm.Auto.Testing.Samples
{
    public class HomePage : Page
    {
        public IElement LoginLink => Get.ById("LoginLink").AsElement();

        public LoginForm LoginForm => Get.ById("LoginForm").AsComponent<LoginForm>();
        public override string Uri => string.Empty;
    }
}