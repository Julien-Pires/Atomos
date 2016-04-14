using System;

namespace Atomos.Tests.Pool
{
    public abstract partial class BasePool_Generic_Test<TPool, TItem, TParam> : IPool_Generic_Test<TPool, TItem>
        where TPool : BasePool<TItem, TParam>, IPool<TItem>
        where TItem : class
    {
        #region Constructors

        protected BasePool_Generic_Test(IPool_Builder<TPool, TItem> builder) : 
            base(builder)
        {
        }

        #endregion

        #region Builder

        protected TPool Build(int capacity = 0, PoolingMode mode = PoolingMode.Strict,
            Func<TItem> initializer = null, Action<TItem> reset = null)
        {
            return ((BasePool_Builder<TPool, TItem>)Builder)
                .WithInitialCapacity(capacity)
                .WithPoolingMode(mode)
                .WithInitializer(initializer)
                .WithReset(reset);
        }

        #endregion

        #region Items Methods

        protected virtual bool CantUseInitializer => false;

        protected abstract int GetValue(TItem item);

        protected abstract TItem CreateItem(int value);

        protected abstract void ResetItem(TItem item);

        #endregion
    }
}