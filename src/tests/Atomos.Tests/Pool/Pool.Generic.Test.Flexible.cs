using Atomos.Atomos;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test_Flexible<T> : Pool_Generic_Test<T>
        where T : class, IPoolItem_Test, new()
    {
        public Pool_Generic_Test_Flexible() : base(PoolingMode.Flexible)
        {
        }
    }
}
