namespace Atomos
{
    internal class StrictPoolGuard<TItem> : IStorageGuard<TItem> where TItem : class
    {
        #region Guards

        public bool MustRegister()
        {
            return true;
        }

        public bool CanGet(IPoolStorage<TItem> storage)
        {
            return true;
        }

        public bool CanSet(TItem item, IPoolStorage<TItem> storage)
        {
            return storage.IsRegistered(item) && !storage.IsAvailable(item);
        }

        #endregion
    }
}