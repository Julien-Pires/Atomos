namespace Atomos
{
    public sealed class ArrayPool<T> : CollectionPool<T[]>
    {
        #region Constructors

        public ArrayPool(CollectionPoolSettings<T[]> settings = null)
        {
        } 

        #endregion

        #region Pooling

        public override T[] Get(int capacity)
        {
            return null;
        }

        #endregion
    }
}