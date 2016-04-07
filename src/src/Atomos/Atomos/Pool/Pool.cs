using System.Collections.Generic;

namespace Atomos
{
    public class Pool<TItem> : BasePool<TItem, object> where TItem : class
    {
        #region Constructors

        /// <summary>
        /// Initialize a new instance of <see cref="BasePool{TItem, TParam}"/> with the specified parameters
        /// </summary>
        /// <param name="settings">Pool parameters</param>
        public Pool(PoolSettings<TItem> settings = null) : this(settings, null)
        {
        }

        protected Pool(PoolSettings<TItem> settings, IPoolStorage<TItem> storage = null, 
            IPoolStorageQuery<TItem, object> query = null, IEnumerable<IPoolGuard<TItem>> guards = null)
            : base(settings,
                  storage ?? new PoolStorage<TItem>(),
                  query ?? DefaultPoolStorageQuery<TItem, object>.Default,
                  guards)
        {
            
        }

        #endregion
    }
}