namespace Atomos
{
    public interface IPoolStorageQuery
    {
        #region Methods

        TItem Search<TItem, TParam>(IPoolStorage<TItem> storage, TParam parameter) where TItem : class;

        #endregion
    }
}