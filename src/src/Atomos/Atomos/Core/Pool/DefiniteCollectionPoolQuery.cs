using System.Collections;

namespace Atomos
{
    internal sealed class DefiniteCollectionPoolQuery<TCollection> : IPoolStorageQuery<TCollection, int?>
        where TCollection : class, ICollection
    {
        #region Fields

        private readonly int _defaultCapacity;

        #endregion

        #region Constructors

        public DefiniteCollectionPoolQuery(int defaultCapacity)
        {
            _defaultCapacity = defaultCapacity;
        } 

        #endregion

        #region Search

        public TCollection Search(IPoolStorage<TCollection> storage, int? parameter)
        {
            return ((KeyedPoolStorage<TCollection, int>)storage).GetByKey(parameter ?? _defaultCapacity);
        }

        #endregion

        #region Insert

        public void Insert(IPoolStorage<TCollection> storage, TCollection item, int? parameter)
        {
            storage.Set(item);
        }

        #endregion
    }
}