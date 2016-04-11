using PoolBuilder = Atomos.Tests.Pool.BasePool_Builder<Atomos.Pool<Atomos.Tests.Pool.PoolItem_Test>, 
    Atomos.Tests.Pool.PoolItem_Test, Atomos.PoolSettings<Atomos.Tests.Pool.PoolItem_Test>>;

namespace Atomos.Tests.Pool
{
    public class Pool_Test : BasePool_Generic_Test<Pool<PoolItem_Test>, PoolItem_Test, object>
    {
        #region Constructors

        public Pool_Test() : base(new PoolBuilder(c => new Pool<PoolItem_Test>(c)))
        {
        }

        #endregion

        #region Builder

        protected override PoolItem_Test CreateItem(int value) => new PoolItem_Test { Value = value };

        #endregion

        #region Items Methods

        protected override bool IsDisposed(PoolItem_Test item) => item.IsDisposed;

        protected override bool IsReset(PoolItem_Test item) => item.IsReset;

        protected override int GetValue(PoolItem_Test item) => item.Value;

        protected override void ResetItem(PoolItem_Test item) => item.Value = 0;

        #endregion
    }
}