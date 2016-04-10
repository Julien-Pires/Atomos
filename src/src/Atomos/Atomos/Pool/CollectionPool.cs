using System;
using System.Collections;

namespace Atomos
{
    public abstract class CollectionPool<TCollection> : BasePool<TCollection, int?>
        where TCollection : class, ICollection
    {
        #region Constructors

        internal CollectionPool(CollectionPoolSettings<TCollection> settings, IPoolItemFactory<TCollection, int?> collectionFactory, 
            ICollectionPoolHelper<TCollection> collectionHelper)
            : base(settings,
                 CreateStorage(settings),
                 CreateQuery(settings),
                 CreateGuard(settings, collectionHelper),
                 collectionFactory)
        {
        }

        #endregion

        #region Initialization

        public static IPoolStorage<TCollection> CreateStorage(CollectionPoolSettings<TCollection> settings)
        {
            IPoolStorage<TCollection> storage;
            CollectionPoolMode mode = settings?.CollectionMode ?? default(CollectionPoolMode);
            switch (mode)
            {
                case CollectionPoolMode.Any:
                case CollectionPoolMode.Fixed:
                    storage = new PoolStorage<TCollection>();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), $"{mode} is not a valid pool collection mode");
            }

            return storage;
        }

        public static IPoolStorageQuery<TCollection, int?> CreateQuery(CollectionPoolSettings<TCollection> settings)
        {
            IPoolStorageQuery<TCollection, int?> query;
            CollectionPoolMode mode = settings?.CollectionMode ?? default(CollectionPoolMode);
            switch (mode)
            {
                case CollectionPoolMode.Any:
                case CollectionPoolMode.Fixed:
                    query = DefaultPoolStorageQuery<TCollection, int?>.Default;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), $"{mode} is not a valid pool collection mode");
            }

            return query;
        }

        public static IPoolGuard<TCollection>[] CreateGuard(CollectionPoolSettings<TCollection> settings, ICollectionPoolHelper<TCollection> collectionHelper)
        {
            IPoolGuard<TCollection>[] guards;
            int initialCapacity = settings?.InitialCapacity ?? 0;
            CollectionPoolMode mode = settings?.CollectionMode ?? default(CollectionPoolMode);
            switch (mode)
            {
                case CollectionPoolMode.Any:
                    guards = new IPoolGuard<TCollection>[0];
                    break;

                case CollectionPoolMode.Fixed:
                    guards = new IPoolGuard<TCollection>[]
                    {
                        new FixedCollectionPoolGuard<TCollection>(initialCapacity, collectionHelper)
                    };
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), $"{mode} is not a valid pool collection mode");
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