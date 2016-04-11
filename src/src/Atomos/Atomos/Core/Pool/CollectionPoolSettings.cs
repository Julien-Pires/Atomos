namespace Atomos
{
    public sealed class CollectionPoolSettings<T> : PoolSettings<T> where T : class
    {
        #region Properties

        public int InitialCapacity { get; set; }

        public CollectionPoolMode CollectionMode { get; set; }

        #endregion

        #region Constructors

        public CollectionPoolSettings()
        {
        }

        public CollectionPoolSettings(CollectionPoolSettings<T> settings) : base(settings)
        {
            InitialCapacity = settings.InitialCapacity;
            CollectionMode = settings.CollectionMode;
        }  

        #endregion
    }
}