using System.Linq;
using Dm.Auto.Testing.Core.Pages;
using Dm.Auto.Testing.Samples.Components;

namespace Dm.Auto.Testing.Samples.Pages
{
    public abstract class CommonPage : Page
    {
        protected abstract string RelativeUrl { get; }
        public override string Uri => RelativeUrl;

        public AccountActionsBlock AccountActions => Find.ByCss(".header-userInfo").Single().AsComponent<AccountActionsBlock>();
    }
}