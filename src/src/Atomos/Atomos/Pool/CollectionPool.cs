using System;
using System.Collections;

namespace Atomos
{
    public abstract class CollectionPool<TCollection> : BasePool<TCollection, int>
        where TCollection : class, ICollection
    {
        #region Constructors

        internal CollectionPool(CollectionPoolSettings<TCollection> settings, 
            CollectionPoolItemHelper<TCollection> collectionHelper)
            : base(settings, 
                 CreateStorage(settings),
                 CreateGuard())
        {
        }

        #endregion

        #region Initialization

        public static IPoolStorage<TCollection> CreateStorage(CollectionPoolSettings<TCollection> settings)
        {
            return null;
        } 

        public static IPoolGuard<TCollection>[] CreateGuard(CollectionPoolSettings<TCollection> settings)
        {
            if (settings == null)
                return null;

            IPoolGuard<TCollection>[] guards;
            switch (settings.CollectionMode)
            {
                case CollectionPoolMode.Fixed:
                    guards = new[] {new FixedCollectionPoolGuard<TCollection>(settings.InitialCapacity, GetCapacity) };
                    break;

                default:
                    throw new ArgumentException($"{settings.CollectionMode} is not a valid pool collection mode");
            }

            return guards;
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