using Dm.Auto.Testing.Core.Browsers;
using Dm.Auto.Testing.Testing;

namespace Dm.Auto.Testing.Samples
{
    public class LoginService : BaseTestingService, ITestingService
    {
        public LoginService(IBrowser browser) : base(browser)
        {
        }

        public void EnterLoginData(string login, string password)
        {
            var loginForm = Browser.GetCurrent<HomePage>().LoginForm;
            loginForm.Login.Value = login;
            loginForm.Password.Value = password;
        }
    }
}