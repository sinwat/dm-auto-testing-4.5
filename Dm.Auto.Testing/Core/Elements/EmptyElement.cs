using Dm.Auto.Testing.Core.Elements.Searchers;
using OpenQA.Selenium;

namespace Dm.Auto.Testing.Core.Elements
{
    public class EmptyElement : IElement
    {
        private ISearchable searchable;

        public EmptyElement(By searchCriteria, ISearchable searchable)
        {
            this.searchable = searchable;
        }

        public bool Exists { get; }
        public string TagName { get; }
        public bool Visible { get; }
        public int Height { get; }
        public int Width { get; }
        public string Text { get; }
        public void Click()
        {
            throw new System.NotImplementedException();
        }

        public void ScrollTo()
        {
            throw new System.NotImplementedException();
        }

        public void HoverMouse()
        {
            throw new System.NotImplementedException();
        }

        public void HoverClick()
        {
            throw new System.NotImplementedException();
        }

        public string GetAttribute(string name)
        {
            throw new System.NotImplementedException();
        }

        public bool HasAttribute(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}