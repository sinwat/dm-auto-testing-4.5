using System;
using Dm.Auto.Testing.Core.Elements.Searchers;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Elements
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException(By searchCriteria, ISearchable searchable)
            : base($"Could not find element by {searchCriteria} in {searchable.GetType().Name}")
        {
        }
    }
}