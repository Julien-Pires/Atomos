using Atomos.Atomos;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test_Strict<T> : Pool_Generic_Test<T>
        where T : Pool_Base_Item_Test, new()
    {
        public Pool_Generic_Test_Strict() : base(new PoolSettings<T> { Mode = PoolingMode.Strict })
        {
        }
    }
}
