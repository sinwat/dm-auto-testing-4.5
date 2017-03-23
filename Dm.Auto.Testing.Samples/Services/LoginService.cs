using Dm.Auto.Testing.Core.Browsers;
using Dm.Auto.Testing.Core.Pages;
using Dm.Auto.Testing.Samples.Pages;
using Dm.Auto.Testing.Testing;

namespace Dm.Auto.Testing.Samples.Services
{
    public class LoginService : BaseTestingService
    {
        public LoginService(IBrowser browser) : base(browser)
        {
        }

        public TPage Login<TPage>(string login, string password)
            where TPage : IPage, new()
        {
            var page = Browser.GetCurrent<CommonPage>();
            page.AccountActions.BeginLogin();
        }
    }
}