using Dm.Auto.Testing.Core.Drivers;
using StructureMap;

namespace Dm.Auto.Testing.Configuration
{
    public static class ContainerConfiguration
    {
        public static Container CreateContainer()
        {
            return new Container(
                c =>
                {
                    c.Scan(
                        scan =>
                        {
                            scan.TheCallingAssembly();
                            scan.WithDefaultConventions();
                            scan.LookForRegistries();
                            scan.AddAllTypesOf<IWebDriverFactory>();
                        });
                });
        }
    }
}