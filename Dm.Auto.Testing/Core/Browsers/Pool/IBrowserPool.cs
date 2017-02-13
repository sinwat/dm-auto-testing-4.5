namespace Dm.Auto.Testing.Core.Browsers.Pool
{
    public interface IBrowserPool
    {
        IBrowser Get();
        void Release(IBrowser browser);
        void RemoveFromPool(IBrowser browser);
    }
}