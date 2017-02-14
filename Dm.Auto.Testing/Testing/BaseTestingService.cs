using Dm.Auto.Testing.Core.Browsers;

namespace Dm.Auto.Testing.Testing
{
    public class BaseTestingService : ITestingService
    {
        protected IBrowser Browser { get; }

        public BaseTestingService(
            IBrowser browser
            )
        {
            Browser = browser;
        }
    }
}