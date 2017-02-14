using Dm.Auto.Testing.Configuration;
using Dm.Auto.Testing.Core.Browsers;
using Dm.Auto.Testing.Core.Browsers.Pool;
using StructureMap;
using TechTalk.SpecFlow;

namespace Dm.Auto.Testing.Testing
{
    [Binding]
    public class ScenarioAspect
    {
        private static readonly object SyncObject = new object();

        [BeforeFeature]
        public static void BeforeFeature()
        {
            BuildConfiguration();
        }

        private static void BuildConfiguration()
        {
            lock (SyncObject)
            {
                if (!HasItem<Container>())
                {
                    SetItem(ContainerConfiguration.CreateContainer());
                }
            }
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var container = GetItem<Container>();
            var browserPool = container.GetInstance<IBrowserPool>();

            SetItem(browserPool.Get());
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var browser = GetItem<IBrowser>();
            if (ScenarioContext.Current.TestError != null)
            {
                try
                {
                    browser.SaveScreenshot();
                }
                // supress any exception because of how Specflow works, it will never free the process,
                // thus the TeamCity run could never be stopped asl
                catch { }
            }
            browser.WebDriver.Dispose();
        }

        public static T GetItem<T>(string key)
        {
            return (T)FeatureContext.Current[key];
        }

        public static T GetItem<T>() where T : class
        {
            return GetItem<T>(GetKey<T>());
        }

        public static void SetItem<T>(string key, T value)
        {
            FeatureContext.Current[key] = value;
        }

        public static void SetItem<T>(T value) where T : class
        {
            SetItem(GetKey<T>(), value);
        }

        public static bool HasItem(string key)
        {
            return FeatureContext.Current.ContainsKey(key);
        }

        public static bool HasItem<T>()
        {
            return HasItem(GetKey<T>());
        }

        private static string GetKey<T>()
        {
            return typeof(T).FullName;
        }
    }
}