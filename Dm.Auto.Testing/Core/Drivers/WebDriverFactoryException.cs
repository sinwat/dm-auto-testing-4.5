using System;
using Dm.Auto.Testing.Core.Browsers;

namespace Dm.Auto.Testing.Core.Drivers
{
    public class WebDriverFactoryException : Exception
    {
        public WebDriverFactoryException(BrowserType browserType) : base($"Factory for {browserType} browser not found")
        {
        }
    }
}