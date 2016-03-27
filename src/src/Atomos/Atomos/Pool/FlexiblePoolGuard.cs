namespace Atomos
{
    internal class FlexiblePoolGuard<T> : IPoolGuard<T> where T : class
    {
        #region Guards

        public bool MustRegister()
        {
            return false;
        }

        public bool CanGet(IPoolStorage<T> storage)
        {
            return true;
        }

        public bool CanSet(T item, IPoolStorage<T> storage)
        {
            return !storage.IsAvailable(item);
        }

        #endregion
    }
}