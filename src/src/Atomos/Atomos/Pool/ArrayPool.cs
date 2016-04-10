namespace Atomos
{
    public sealed partial class ArrayPool<T> : CollectionPool<T[]>
    {
        #region Fields

        private static readonly ArrayPoolHelper Helper = new ArrayPoolHelper();

        #endregion

        #region Constructors

        public ArrayPool(CollectionPoolSettings<T[]> settings = null)
            : base(settings, 
                  new CollectionPoolItemFactory<T[]>(c => new T[c], (settings?.InitialCapacity).GetValueOrDefault()), 
                  Helper)
        {
        }

        #endregion
    }
}