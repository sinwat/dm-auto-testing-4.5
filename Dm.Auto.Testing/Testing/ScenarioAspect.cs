using Dm.Auto.Testing.Configuration;
using Dm.Auto.Testing.Core.Browsers;
using Dm.Auto.Testing.Core.Browsers.Pool;
using Dm.Auto.Testing.Core.Utils;
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
            
            var browser = browserPool.Get();
            browser.WebDriver.Manage().Window.Maximize();
            SetItem(browser, ScenarioContext.Current);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var browser = GetItem<IBrowser>(ScenarioContext.Current);
            if (ScenarioContext.Current.TestError != null)
            {
                try
                {
                    browser.SaveScreenshot();
                }
                // supress any exception because of how Specflow works, it will never free the process,
                // thus the TeamCity run could never be stopped asl
                catch
                {
                }
            }

            ReleaseItem<IBrowser>(ScenarioContext.Current);
            browser.Dispose();

            Wait.For(2000);
        }

        public static T GetItem<T>(string key, SpecFlowContext context = null)
        {
            return (T)(context ?? FeatureContext.Current)[key];
        }

        public static T GetItem<T>(SpecFlowContext context = null) where T : class
        {
            return GetItem<T>(GetKey<T>(), context);
        }

        public static void SetItem<T>(string key, T value, SpecFlowContext context = null)
        {
            (context ?? FeatureContext.Current).Set(value, key);
        }

        public static void SetItem<T>(T value, SpecFlowContext context = null) where T : class
        {
            SetItem(GetKey<T>(), value, context);
        }

        public static bool HasItem(string key, SpecFlowContext context = null)
        {
            return (context ?? FeatureContext.Current).ContainsKey(key);
        }

        public static bool HasItem<T>(SpecFlowContext context = null)
        {
            return HasItem(GetKey<T>());
        }

        private static string GetKey<T>()
        {
            return typeof(T).FullName;
        }

        public static void ReleaseItem<T>(SpecFlowContext context = null) where T : class
        {
            (context ?? FeatureContext.Current).Remove(GetKey<T>());
        }
    }
}