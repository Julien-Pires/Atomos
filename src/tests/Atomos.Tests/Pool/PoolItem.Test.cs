namespace Atomos.Tests.Pool
{
    public interface IPoolItem_Test
    {
        #region Properties

        int Value { get; set; }

        #endregion
    }

    public class PoolItem_Test : IPoolItem_Test
    {
        #region Properties

        public int Value { get; set; }

        #endregion
    }
}