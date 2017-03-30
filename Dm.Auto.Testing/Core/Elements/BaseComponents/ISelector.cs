namespace Dm.Auto.Testing.Core.Elements.BaseComponents
{
    public interface ISelector : IElement
    {
        string Selected { get; }
        string[] Options();

        void Select(int index);
        void Select(string text);
        void SelectFirstThatStartsWith(string partText);
        void SelectRandom();
    }
}
