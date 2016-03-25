namespace Atomos
{
    public interface IPoolGuard<T> where T : class
    {
        bool CanGet(IPoolStorage<T> storage);

        bool CanSet(T item, IPoolStorage<T> storage);
    }
}