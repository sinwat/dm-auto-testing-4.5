using Dm.Auto.Testing.Core.Browsers;
using StructureMap;
using TechTalk.SpecFlow;

namespace Dm.Auto.Testing.Tests
{
    [Binding]
    public abstract class FeatureStepsBase
    {
        public static void AppendContainerInstance<T>() where T : class
        {
            if (ScenarioAspect.HasItem<T>())
            {
                return;
            }

            var instance = SafeContainer.GetInstance<T>();
            ScenarioAspect.SetItem(instance);
        }

        protected static IBrowser Browser => ScenarioAspect.GetItem<IBrowser>();

        private static Container SafeContainer
        {
            get
            {
                if (!ScenarioAspect.HasItem<Container>())
                {
                    ScenarioAspect.BeforeFeature();
                }
                return ScenarioAspect.GetItem<Container>();
            }
        }
    }
}