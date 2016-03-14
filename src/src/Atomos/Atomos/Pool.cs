using System;
using System.Collections.Generic;

namespace Atomos.Atomos
{
    public class Pool<T> : IDisposable where T : class
    {
        #region Fields

        private IPoolStorage<T> _storage;
        private readonly Func<T> _initializer = New<T>.Create;
        private readonly Action<T> _reset = c => { };

        #endregion

        #region Properties

        public int Count
        {
            get { return _storage.Count; }
        }

        #endregion

        #region Constructors

        public Pool(PoolSettings<T>? settings = null) : this(settings, CreateStorage)
        {
        }

        protected Pool(PoolSettings<T>? settings, Func<PoolSettings<T>, IPoolStorage<T>> poolInitializer)
        {
            if (poolInitializer == null)
                throw new ArgumentNullException(nameof(poolInitializer));

            PoolSettings<T> settingsValue = settings.HasValue ? settings.Value : default(PoolSettings<T>);
            _initializer = _initializer ?? settingsValue.Initializer;
            _reset = _reset ?? settingsValue.Reset;
            _storage = poolInitializer(settingsValue);
            _storage.SetCapacity(settingsValue.Capacity);
        }

        #endregion

        #region Initialize

        private static IPoolStorage<T> CreateStorage(PoolSettings<T> settings)
        {
            PoolingMode mode = settings.Mode;
            IPoolStorage<T> storage;
            switch(mode)
            {
                case PoolingMode.Strict:
                    storage = new StrictPoolStorage<T>();
                    break;

                default:
                    throw new ArgumentException($"{mode} is not a valid pooling mode");
            }

            return storage;
        }

        #endregion

        #region Cleanup

        public void Reset(bool destroyItems = false)
        {
            _storage.Reset(destroyItems);

            if (!destroyItems)
            {
                foreach (T item in _storage)
                    _reset(item);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Pooling

        public T Get()
        {
            T item = _storage.Get();
            if (item != null)
                return item;

            _storage.Register(_initializer());
            item = _storage.Get();

            return item;
        }

        public void Set(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _reset(item);
            _storage.Set(item);
        }

        public void Set(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach(T item in items)
                Set(item);
        }

        #endregion
    }
}