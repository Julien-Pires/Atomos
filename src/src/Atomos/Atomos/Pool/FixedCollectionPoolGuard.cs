using System;
using System.Collections;

namespace Atomos
{
    internal class FixedCollectionPoolGuard<TCollection> : IPoolGuard<TCollection> where TCollection : class, ICollection
    {
        #region Fields

        private readonly int _capacity;
        private readonly Func<TCollection, int> _getCapacity;

        #endregion

        #region Constructors

        public FixedCollectionPoolGuard(int capacity, Func<TCollection, int> getCapacity)
        {
            _capacity = capacity;
            _getCapacity = getCapacity;
        }

        #endregion

        #region Guard

        public bool CanGet(IPoolStorage<TCollection> storage)
        {
            return true;
        }

        public bool CanSet(TCollection item, IPoolStorage<TCollection> storage)
        {
            return _getCapacity(item) == _capacity;
        }

        #endregion
    }
}