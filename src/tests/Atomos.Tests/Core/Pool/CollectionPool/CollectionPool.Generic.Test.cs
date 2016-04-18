using System;
using System.Collections;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test<TPool, TItem> : BasePool_Generic_Test<TPool, TItem, int?>
        where TPool : CollectionPool<TItem>, IPool<TItem>
        where TItem : class, ICollection
    {
        #region Properties

        protected override bool CantUseInitializer => true;

        #endregion

        #region Constructors

        protected CollectionPool_Generic_Test(Func<PoolSettings<TItem>,  TPool> factory)
            : base(new CollectionPool_Builder<TPool, TItem>(factory))
        {
        }

        #endregion

        #region Builder

        protected TPool Build(CollectionPoolMode collectionMode = CollectionPoolMode.Any,
            int initialCapacity = 0, PoolingMode mode = PoolingMode.Strict, int initialPoolCapacity = 0)
        {
            return (Builder as CollectionPool_Builder<TPool, TItem>)?
                .WithCollectionPoolMode(collectionMode)
                .WithCollectionCapacity(initialCapacity)
                .WithPoolingMode(mode)
                .WithInitialCapacity(initialPoolCapacity);
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

        #endregion
    }
}