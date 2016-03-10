using Atomos.Atomos;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<T> where T : Pool_Base_Item_Test, new()
    {
        #region Fields

        private Pool<T> _pool;

        #endregion

        #region Constructors

        public Pool_Generic_Test()
        {
            _pool = new Pool<T>();
        }

        #endregion
    }

    public abstract class Pool_Base_Item_Test
    {
        public int Value { get; set; }
    }
}
