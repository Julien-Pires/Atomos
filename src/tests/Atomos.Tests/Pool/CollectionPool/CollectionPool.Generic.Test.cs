using System;
using System.Collections;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem> : IPool_Generic_Test<TPool, TItem>
        where TPool : CollectionPool<TItem>, IPool<TItem>
        where TItem : class, ICollection
    {
        #region Fields

        private readonly Func<CollectionPoolSettings<TItem>, TPool> _factory;

        #endregion

        #region Constructors

        protected CollectionPool_Generic_Test(Func<CollectionPoolSettings<TItem>,  TPool> factory)
            : base(() => factory(null))
        {
            _factory = factory;
        }

        #endregion

        #region Builder

        protected TPool Build(CollectionPoolMode collectionMode = CollectionPoolMode.Any,
            int initialCapacity = 0, PoolingMode mode = PoolingMode.Strict)
        {
            return new CollectionPool_Builder<TPool, TItem>(_factory)
                .WithCollectionPoolMode(collectionMode)
                .WithInitialCapacity(initialCapacity)
                .WithPoolingMode(mode);
        }

        #endregion

        #region Items Validation

        protected override bool IsDisposed(TItem item)
        {
            return true;
        }

        protected override bool IsReset(TItem item)
        {
            return true;
        }

        protected abstract int GetCapacity(TItem pool);

        protected abstract TItem CreateItem(int capacity);

        #endregion
    }
}