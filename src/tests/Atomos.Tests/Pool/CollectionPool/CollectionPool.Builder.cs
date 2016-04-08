using System;
using System.Collections;

namespace Atomos.Tests.Pool
{
    public sealed class CollectionPool_Builder<TPool, TItem> : BasePool_Builder<TPool, TItem, CollectionPoolSettings<TItem>>
        where TPool : CollectionPool<TItem>, IPool<TItem>
        where TItem : class, ICollection
    {
        #region Fields

        private int _initialCapacity;
        private CollectionPoolMode _collectionMode;

        #endregion

        #region Constructors

        public CollectionPool_Builder(Func<CollectionPoolSettings<TItem>, TPool> factory) : base(factory)
        {
        }

        #endregion

        #region Methods

        protected override CollectionPoolSettings<TItem> CreateSettings()
        {
            return new CollectionPoolSettings<TItem>();
        }

        protected override CollectionPoolSettings<TItem> PrepareSettings()
        {
            CollectionPoolSettings<TItem> settings = base.PrepareSettings();
            settings.InitialCapacity = _initialCapacity;
            settings.CollectionMode = _collectionMode;

            return settings;
        }

        public CollectionPool_Builder<TPool, TItem> WithCollectionCapacity(int initialCapacity)
        {
            _initialCapacity = initialCapacity;

            return this;
        }

        public CollectionPool_Builder<TPool, TItem> WithCollectionPoolMode(CollectionPoolMode mode)
        {
            _collectionMode = mode;

            return this;
        }

        #endregion
    }
}