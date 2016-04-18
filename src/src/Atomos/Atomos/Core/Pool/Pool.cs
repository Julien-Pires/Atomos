namespace Atomos
{
    public class Pool<TItem> : BasePool<TItem, object> where TItem : class
    {
        #region Constructors

        /// <summary>
        /// Initialize a new instance of <see cref="BasePool{TItem, TParam}"/> with the specified parameters
        /// </summary>
        /// <param name="settings">Pool parameters</param>
        public Pool(PoolSettings<TItem> settings = null) :
            base(new PoolStorage<TItem>(),
                  new DefaultPoolStorageQuery<TItem, object>(),
                  settings:settings)
        {
        }

        #endregion
    }
}