using System;
using System.Collections;
using System.Linq;

namespace Atomos
{
    public abstract class CollectionPool<TCollection> : BasePool<TCollection, int?>
        where TCollection : class, ICollection
    {
        #region Fields

        public static readonly CollectionPoolSettings<TCollection> DefaultSettings = new CollectionPoolSettings<TCollection>(); 

        #endregion

        #region Constructors

        internal CollectionPool(CollectionPoolSettings<TCollection> settings, IPoolItemFactory<TCollection, int?> collectionFactory, 
            ICollectionPoolHelper<TCollection> collectionHelper)
            : base(ValidateSettings(settings),
                 CreateStorage(settings ?? DefaultSettings, collectionHelper),
                 CreateQuery(settings ?? DefaultSettings),
                 CreateGuard(settings ?? DefaultSettings, collectionHelper),
                 collectionFactory)
        {
        }

        #endregion

        #region Validation

        private static CollectionPoolSettings<TCollection> ValidateSettings(CollectionPoolSettings<TCollection> settings)
        {
            if (settings == null)
                return null;

            if(settings.InitialCapacity < 0)
                throw new ArgumentOutOfRangeException(nameof(settings.InitialCapacity), "Collection initial capacity must be positive");

            if(Enum.GetValues(typeof (CollectionPoolMode)).Cast<CollectionPoolMode>().All(c => c != settings.CollectionMode))
                throw new PoolException($"{settings.CollectionMode} is not a valid pool collection mode");

            return settings;
        }

        #endregion

        #region Initialization

        public static IPoolStorage<TCollection> CreateStorage(CollectionPoolSettings<TCollection> settings,
            ICollectionPoolHelper<TCollection> collectionHelper)
        {
            IPoolStorage<TCollection> storage;
            switch (settings.CollectionMode)
            {
                case CollectionPoolMode.Any:
                case CollectionPoolMode.Fixed:
                    storage = new PoolStorage<TCollection>();
                    break;

                case CollectionPoolMode.Definite:
                    storage = new KeyedPoolStorage<TCollection, int>(new DefaultKeySelector<TCollection, int>(collectionHelper.GetCapacity));
                    break;

                default:
                    throw new PoolException($"{settings.CollectionMode} is not a valid pool collection mode");
            }

            return storage;
        }

        public static IPoolStorageQuery<TCollection, int?> CreateQuery(CollectionPoolSettings<TCollection> settings)
        {
            IPoolStorageQuery<TCollection, int?> query;
            switch (settings.CollectionMode)
            {
                case CollectionPoolMode.Any:
                case CollectionPoolMode.Fixed:
                    query = DefaultPoolStorageQuery<TCollection, int?>.Default;
                    break;

                case CollectionPoolMode.Definite:
                    query = new DefiniteCollectionPoolQuery<TCollection>(settings.InitialCapacity);
                    break;

                default:
                    throw new PoolException($"{settings.CollectionMode} is not a valid pool collection mode");
            }

            return query;
        }

        public static IPoolGuard<TCollection>[] CreateGuard(CollectionPoolSettings<TCollection> settings, 
            ICollectionPoolHelper<TCollection> collectionHelper)
        {
            IPoolGuard<TCollection>[] guards;
            switch (settings.CollectionMode)
            {
                case CollectionPoolMode.Any:
                case CollectionPoolMode.Definite:
                    guards = new IPoolGuard<TCollection>[0];
                    break;

                case CollectionPoolMode.Fixed:
                    guards = new IPoolGuard<TCollection>[]
                    {
                        new FixedCollectionPoolGuard<TCollection>(settings.InitialCapacity, collectionHelper)
                    };
                    break;

                default:
                    throw new PoolException($"{settings.CollectionMode} is not a valid pool collection mode");
            }

            return guards;
        }

        #endregion

        #region Pooling

        public TCollection Get(int capacity)
        {
            return Get((int?)capacity);
        }

        #endregion
    }
}