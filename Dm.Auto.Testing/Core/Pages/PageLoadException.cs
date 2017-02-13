using System;

namespace Dm.Auto.Testing.Core.Pages
{
    public class PageLoadException<TPage> : Exception
        where TPage : IPage
    {
        public PageLoadException(TPage page) : base($"Unable to load page {typeof(TPage).Name} at url {page.Url}")
        {
        }
    }
}