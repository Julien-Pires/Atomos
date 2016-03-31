using System;
using System.Collections;

namespace Atomos.Tests.Pool
{
    public abstract class CollectionPool_Generic_Test<TPool, TItem> : IPool_Generic_Test<TPool, TItem>
        where TPool : CollectionPool<TItem>, IPool<TItem>
        where TItem : class, ICollection
    {
        #region Fields

        protected readonly CollectionPool_Builder<TItem> Builder;

        #endregion

        #region Constructors

        protected CollectionPool_Generic_Test(Func<CollectionPoolSettings<TItem>,  TPool> factory, CollectionPoolMode mode)
            : base(() => factory(null))
        {
            Builder = new CollectionPool_Builder<TItem>(factory).WithCollectionPoolMode(mode);
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