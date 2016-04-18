namespace Atomos
{
    internal class FlexiblePoolGuard<TItem> : IStorageGuard, IPoolGuard<TItem> 
        where TItem : class 
    {
        #region Guards

        public bool MustRegister()
        {
            return false;
        }

        public bool CanGet(IPoolStorage<TItem> storage)
        {
            return true;
        }

        public bool CanSet(TItem item, IPoolStorage<TItem> storage)
        {
            return !storage.IsAvailable(item);
        }

        #endregion
    }
}