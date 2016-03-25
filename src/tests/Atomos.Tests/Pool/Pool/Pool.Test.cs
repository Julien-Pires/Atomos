namespace Atomos.Tests.Pool
{
    public sealed class Pool_Strict_Test_PoolItem : Pool_Generic_Test_Strict<Pool<PoolItem_Test>, PoolItem_Test>
    {
        public Pool_Strict_Test_PoolItem() : base(c => new Pool<PoolItem_Test>(c))
        {
        }
    }

    public sealed class Pool_Flexible_Test_PoolItem : Pool_Generic_Test_Flexible<Pool<PoolItem_Test>, PoolItem_Test>
    {
        public Pool_Flexible_Test_PoolItem() : base(c => new Pool<PoolItem_Test>(c))
        {
        }
    }
}