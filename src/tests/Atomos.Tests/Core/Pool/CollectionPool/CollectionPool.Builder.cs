using System;
using System.Collections;

namespace Atomos.Tests.Pool
{
    public sealed class CollectionPool_Builder<TPool, TItem> : BasePool_Builder<TPool, TItem>
        where TPool : CollectionPool<TItem>, IPool<TItem>
        where TItem : class, ICollection
    {
        #region Fields

        private int _initialCapacity;
        private CollectionPoolMode _collectionMode;

        #endregion

        #region Constructors

        public CollectionPool_Builder(Func<PoolSettings<TItem>, TPool> factory) : base(factory)
        {
        }

        #endregion

        #region Methods

        protected override object CreateSettings()
        {
            return new CollectionPoolSettings<TItem>();
        }

        protected override object PrepareSettings()
        {
            CollectionPoolSettings<TItem> settings = (CollectionPoolSettings<TItem>)base.PrepareSettings();
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