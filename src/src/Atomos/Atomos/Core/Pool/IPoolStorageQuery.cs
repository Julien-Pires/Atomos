namespace Atomos
{
    public interface IPoolStorageQuery<TItem, in TParam> where TItem : class
    {
        #region Methods

        TItem Search(IPoolStorage<TItem> storage, TParam parameter);

        void Insert(IPoolStorage<TItem> storage, TItem item, TParam parameter);

        #endregion
    }
}