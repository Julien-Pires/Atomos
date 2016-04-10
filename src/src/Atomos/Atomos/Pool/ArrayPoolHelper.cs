namespace Atomos
{
    public sealed partial class ArrayPool<T>
    {
        internal class ArrayPoolHelper : ICollectionPoolHelper<T[]>
        {
            #region Helpers

            public int GetCapacity(T[] item)
            {
                return item.Length;
            }

            #endregion
        }
    }
}