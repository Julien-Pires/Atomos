using System;
using System.Collections;

namespace Atomos
{
    internal sealed class CollectionPoolItemFactory<TCollection> : IPoolItemFactory<TCollection, int?>
        where TCollection : class, ICollection
    {
        #region Properties

        private readonly Func<int, TCollection> _factory;
        private readonly int _capacity;
        private readonly bool _useParameter;

        #endregion

        #region Constructors

        public CollectionPoolItemFactory(Func<int, TCollection> factory, CollectionPoolSettings<TCollection> settings)
        {
            _factory = factory;
            _capacity = (settings?.InitialCapacity).GetValueOrDefault();
            _useParameter = (settings?.CollectionMode ?? CollectionPoolMode.Any) == CollectionPoolMode.Definite;
        }

        #endregion

        #region Initialization

        public TCollection Create(int? parameter)
        {
            return _factory(_useParameter ? (parameter ?? _capacity) : _capacity);
        }

        #endregion
    }
}