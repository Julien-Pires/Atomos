using System;
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

        #region Initialization

        public static IPoolGuard CreateGuard(CollectionPoolSettings<TCollection> settings)
        {
            if (settings == null)
                return null;

            switch (settings.CollectionMode)
            {
                default:
                    throw new ArgumentException($"{settings.CollectionMode} is not a valid pool collection mode");
            }
        }

        public abstract TCollection Create(int capacity);

        #endregion

        #region Pooling

        public TCollection Get(int capacity)
        {
            return null;
        }

        #endregion
    }
}