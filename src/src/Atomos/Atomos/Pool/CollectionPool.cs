using System.Collections;

namespace Atomos
{
    public abstract class CollectionPool<TCollection> : Pool<TCollection>
        where TCollection : class, ICollection
    {
        #region Constructors

        protected CollectionPool(CollectionPoolSettings<TCollection> settings = null)
        {
        }

        #endregion

        #region Pooling

        public TCollection Get(int capacity)
        {
            return null;
        }

        #endregion
    }
}