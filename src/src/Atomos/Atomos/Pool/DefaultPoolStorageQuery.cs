namespace Atomos
{
    internal sealed class DefaultPoolStorageQuery<TItem, TParam> : IPoolStorageQuery<TItem, TParam> where TItem : class
    {
        #region Fields

        public static readonly DefaultPoolStorageQuery<TItem, TParam> Default = new DefaultPoolStorageQuery<TItem, TParam>();

        #endregion

        #region Constructors

        private DefaultPoolStorageQuery()
        {
        }

        #endregion

        #region Search

        public TItem Search(IPoolStorage<TItem> storage, TParam parameter)
        {
            return storage.Count > 0 ? storage[storage.Count - 1] : null;
        }

        #endregion
    }
}