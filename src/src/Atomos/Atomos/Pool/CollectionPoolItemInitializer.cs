using System;
using System.Collections;

namespace Atomos
{
    internal sealed class CollectionPoolItemInitializer<TCollection> where TCollection : class, ICollection
    {
        #region Properties

        private readonly Func<int, TCollection> _initializer;
        private readonly int _capacity;
        private readonly bool _useParameter;

        #endregion

        #region Constructors

        public CollectionPoolItemInitializer(Func<int, TCollection> initializer, int capacity, bool useParameter = false)
        {
            _initializer = initializer;
            _capacity = capacity;
            _useParameter = useParameter;
        }

        #endregion

        #region Initialization

        public TCollection Create<TParam>(TParam parameter)
        {
            return _initializer(_useParameter ? (int) parameter : _capacity);
        }

        #endregion
    }
}