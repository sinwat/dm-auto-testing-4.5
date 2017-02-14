using Dm.Auto.Testing.Core.Browsers;
using StructureMap;
using TechTalk.SpecFlow;

namespace Dm.Auto.Testing.Testing
{
    [Binding]
    public abstract class FeatureStepsBase
    {
        public static T AppendContainerInstance<T>() where T : class, ITestingService
        {
            if (ScenarioAspect.HasItem<T>())
            {
                return ScenarioAspect.GetItem<T>();
            }

            var container = SafeContainer;
            container.Configure(c =>
            {
                c.For<IBrowser>().Use(t => Browser);
            });
            var instance = container.GetInstance<T>();
            ScenarioAspect.SetItem(instance);
            container.Release(instance);
            return instance;
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