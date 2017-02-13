using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Dm.Auto.Testing.Core.Browsers.Factories;

namespace Dm.Auto.Testing.Core.Browsers.Pool
{
    public class BrowserPool : IBrowserPool
    {
        private readonly IBrowserFactory browserFactory;

        private int DegreeOfParallelism => int.Parse(ConfigurationManager.AppSettings["DegreeOfParallelism"]);

        private readonly List<IBrowser> browsers;
        private readonly Semaphore semaphore;

        public BrowserPool(
            IBrowserFactory browserFactory
            )
        {
            this.browserFactory = browserFactory;
            var degreeOfParallelism = DegreeOfParallelism;
            browsers = new List<IBrowser>();
            semaphore = new Semaphore(degreeOfParallelism, degreeOfParallelism);
        }

        public IBrowser Get()
        {
            semaphore.WaitOne();
            try
            {
                lock (browsers)
                {
                    var browser = browsers.FirstOrDefault() ?? browserFactory.Create(BrowserType.Webkit);
                    browsers.Remove(browser);
                    return browser;
                }
            }
            catch (Exception)
            {
                semaphore.Release();
                throw;
            }
        }

        public void Release(IBrowser browser)
        {
            lock (browsers)
            {
                browsers.Add(browser);

                var freeBrowsersCount = semaphore.Release() + 1;
                if (freeBrowsersCount == DegreeOfParallelism)
                {
                    browsers.ForEach(b => b.Dispose());
                    browsers.Clear();
                }
            }
        }

        public void RemoveFromPool(IBrowser browser)
        {
            semaphore.Release();
        }
    }
}