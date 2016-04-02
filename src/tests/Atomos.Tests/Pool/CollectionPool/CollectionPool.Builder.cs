using System;
using System.Collections;

namespace Atomos.Tests.Pool
{
    public sealed class CollectionPool_Builder<TPool, TItem>
        where TPool : CollectionPool<TItem>, IPool<TItem>
        where TItem : class, ICollection
    {
        #region Fields

        private readonly Func<CollectionPoolSettings<TItem>, TPool> _factory; 
        private int _initialCapacity;
        private CollectionPoolMode _collectionMode;
        private PoolingMode _poolMode;

        #endregion

        #region Constructors

        public CollectionPool_Builder(Func<CollectionPoolSettings<TItem>, TPool> factory)
        {
            _factory = factory;
        }

        #endregion

        #region Methods

        public TPool Build()
        {
            return _factory(new CollectionPoolSettings<TItem>
            {
                InitialCapacity = _initialCapacity,
                CollectionMode = _collectionMode,
                Mode = _poolMode
            });
        }

        public static implicit operator TPool(CollectionPool_Builder<TPool, TItem> builder)
        {
            return builder.Build();
        }

        public CollectionPool_Builder<TPool, TItem> WithInitialCapacity(int initialCapacity)
        {
            _initialCapacity = initialCapacity;

            return this;
        }

        public CollectionPool_Builder<TPool, TItem> WithCollectionPoolMode(CollectionPoolMode mode)
        {
            _collectionMode = mode;

            return this;
        }

        public CollectionPool_Builder<TPool, TItem> WithPoolingMode(PoolingMode mode)
        {
            _poolMode = mode;

            return this;
        }

        #endregion
    }
}