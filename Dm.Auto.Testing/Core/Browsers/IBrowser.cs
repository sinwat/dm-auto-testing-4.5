using System;
using Dm.Auto.Testing.Core.Pages;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Browsers
{
    public interface IBrowser : IDisposable
    {
        IWebDriver WebDriver { get; }

        TPage GetCurrent<TPage>() where TPage : class, IPage;
        TPage GoTo<TPage>(string queryParams = null) where TPage : IPage, new();

        void SaveScreenshot();
    }
}