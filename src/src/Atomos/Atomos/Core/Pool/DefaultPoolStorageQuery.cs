namespace Atomos
{
    public sealed class DefaultPoolStorageQuery<TItem, TParam> : IPoolStorageQuery<TItem, TParam> 
        where TItem : class
    {
        #region Search

        public TItem Search(IPoolStorage<TItem> storage, TParam parameter)
        {
            return storage.Get();
        }

        #endregion

        #region Insert

        public void Insert(IPoolStorage<TItem> storage, TItem item, TParam parameter)
        {
            storage.Set(item);
        }

        #endregion
    }
}