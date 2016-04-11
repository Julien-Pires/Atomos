using System.Collections;

namespace Atomos
{
    internal class FixedCollectionPoolGuard<TCollection> : IPoolGuard<TCollection> 
        where TCollection : class, ICollection
    {
        #region Fields

        private readonly int _capacity;
        private readonly ICollectionPoolHelper<TCollection> _collectionHelper;

        #endregion

        #region Constructors

        public FixedCollectionPoolGuard(int capacity, ICollectionPoolHelper<TCollection> collectionHelper)
        {
            _capacity = capacity;
            _collectionHelper = collectionHelper;
        }

        #endregion

        #region Guard

        public bool CanGet(IPoolStorage<TCollection> storage)
        {
            return true;
        }

        public bool CanSet(TCollection item, IPoolStorage<TCollection> storage)
        {
            return _collectionHelper.GetCapacity(item) == _capacity;
        }

        #endregion
    }
}