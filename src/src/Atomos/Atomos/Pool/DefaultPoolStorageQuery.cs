namespace Atomos
{
    internal sealed class DefaultPoolStorageQuery : IPoolStorageQuery
    {
        #region Fields

        public static readonly DefaultPoolStorageQuery Default = new DefaultPoolStorageQuery();

        #endregion

        #region Constructors

        private DefaultPoolStorageQuery()
        {
        }

        #endregion

        #region Search

        public TItem Search<TItem, TParam>(IPoolStorage<TItem> storage, TParam parameter) where TItem : class
        {
            return storage.Count > 0 ? storage[storage.Count - 1] : null;
        }

        #endregion
    }
}