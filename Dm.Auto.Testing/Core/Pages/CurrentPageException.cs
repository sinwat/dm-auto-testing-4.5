﻿using System;

namespace Dm.Auto.Testing.Core.Pages
{
    public class CurrentPageException<TPage> : Exception
        where TPage : IPage
    {
        public CurrentPageException() : base($"There is no current page right now. Use \"GoTo<{typeof(TPage).Name}>\" browser methods before!")
        {
        }

        public CurrentPageException(string url) : base($"Current page cannot be converted to {typeof(TPage).Name}. Browser is at {url} right now.")
        {
        }
    }
}