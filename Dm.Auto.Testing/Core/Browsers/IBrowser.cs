using System;
using Dm.Auto.Testing.Core.Pages;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Browsers
{
    public interface IBrowser : IDisposable
    {
        IWebDriver WebDriver { get; }

        TPage GetCurrent<TPage>() where TPage : class, IPage;
        TPage GetCurrentUnsafe<TPage>() where TPage : class, IPage, new();
        TPage GoTo<TPage>(string queryParams = null) where TPage : class, IPage, new();
        TPage WaitForSubmit<TPage>() where TPage : class, IPage, new();

        void PrepareForAjaxRequest();
        void WaitForPendingAjaxRequests();
        string ExecuteScript(string script);

        void SaveScreenshot();
    }
}