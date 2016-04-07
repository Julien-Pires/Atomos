using System;
using System.Collections;

namespace Atomos
{
    internal sealed class CollectionPoolItemHelper<TCollection> : IPoolItemInitializer<TCollection, int?>
        where TCollection : class, ICollection
    {
        #region Properties

        private readonly Func<int?, TCollection> _factory;
        private readonly Func<TCollection, int> _getCapacity; 
        private readonly int _capacity;
        private readonly bool _useParameter;

        #endregion

        #region Constructors

        public CollectionPoolItemHelper(Func<int?, TCollection> factory, Func<TCollection, int> getCapacity,
            int capacity, bool useParameter = false)
        {
            _factory = factory;
            _getCapacity = getCapacity;

            _capacity = capacity;
            _useParameter = useParameter;
        }

        #endregion

        #region Initialization

        public TCollection Create(int? parameter)
        {
            return _factory(_useParameter ? (parameter ?? _capacity) : _capacity);
        }

        public int GetCapacity(TCollection item)
        {
            return _getCapacity(item);
        }

        #endregion
    }
}