using Atomos.Atomos;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<T> where T : Pool_Base_Item_Test, new()
    {
        #region Fields

        protected readonly Pool<T> Pool;

        #endregion

        #region Properties

        protected PoolingMode Mode { get; }

        #endregion

        #region Constructors

        protected Pool_Generic_Test(PoolingMode mode)
        {
            Pool = new Pool<T>(new PoolSettings<T> { Mode = mode });
            Mode = mode;
        }

        #endregion
    }

    public abstract class Pool_Base_Item_Test
    {
        public int Value { get; set; }
    }
}
