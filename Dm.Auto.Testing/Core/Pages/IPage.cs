using Dm.Auto.Testing.Core.Browsers;
using Dm.Auto.Testing.Core.Elements.Searchers;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Pages
{
    public interface IPage : ISearchable
    {
        string Uri { get; } // constant page-dependent identifier
        string Url { get; } // current page url, that may depend on query parameters
        bool IsError { get; }
        bool IsLoaded { get; }

        void Initialize(IWebDriver webDriver, IBrowser browser);
    }
}