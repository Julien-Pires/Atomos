using System;
using System.Collections;

namespace Atomos.Tests.Pool
{
    public sealed class CollectionPool_Builder<TItem> where TItem : class, ICollection
    {
        #region Fields

        private readonly Func<CollectionPoolSettings<TItem>, CollectionPool<TItem>> _factory; 
        private int _initialCapacity;
        private CollectionPoolMode _collectionMode;
        private PoolingMode _poolMode;

        #endregion

        #region Constructors

        public CollectionPool_Builder(Func<CollectionPoolSettings<TItem>, CollectionPool<TItem>> factory)
        {
            _factory = factory;
        }

        #endregion

        #region Methods

        public CollectionPool<TItem> Build()
        {
            return _factory(new CollectionPoolSettings<TItem>
            {
                InitialCapacity = _initialCapacity,
                CollectionMode = _collectionMode,
                Mode = _poolMode
            });
        }

        public static implicit operator CollectionPool<TItem>(CollectionPool_Builder<TItem> builder)
        {
            return builder.Build();
        }

        public CollectionPool_Builder<TItem> WithInitialCapacity(int initialCapacity)
        {
            _initialCapacity = initialCapacity;

            return this;
        }

        public CollectionPool_Builder<TItem> WithCollectionPoolMode(CollectionPoolMode mode)
        {
            _collectionMode = mode;

            return this;
        }

        public CollectionPool_Builder<TItem> WithPoolingMode(PoolingMode mode)
        {
            _poolMode = mode;

            return this;
        }

        #endregion
    }
}