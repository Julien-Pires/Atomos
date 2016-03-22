using System;
using System.Reflection;
using System.Collections.Generic;

namespace Atomos.Atomos
{
    /// <summary>
    /// Implements a generic object pool. The pool will automatically create new elements
    /// when required. By default the pool return policy is "Strict", it means that a
    /// pool can only accept element that has been created by the pool. "Flexible" mode allow
    /// to return any object to the pool.
    /// </summary>
    /// <typeparam name="T">Type of pool elements</typeparam>
    public class Pool<T> : IDisposable where T : class
    {
        #region Fields

        private static readonly Action<T> EmptyAction = c => { };
        private static readonly Action<T> DisposeAction = c => (c as IDisposable).Dispose();

        private IPoolStorage<T> _storage;
        private bool _isDisposed;
        private readonly Func<T> _initializer;
        private readonly Action<T> _reset;
        private readonly Action<T> _dispose;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of available elements in the pool
        /// </summary>
        public int Count
        {
            get { return _storage.Count; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize a new instance of Pool<T> with the specified parameters
        /// </summary>
        /// <param name="settings">Pool parameters</param>
        public Pool(PoolSettings<T>? settings = null) : this(settings, CreateStorage)
        {
        }

        /// <summary>
        /// Initialize a new instance of Pool<T> with the specified parameters and storage factory delegate
        /// </summary>
        /// <param name="settings">Pool parameter</param>
        /// <param name="storageInitializer">Delegate used to initialize the pool storage</param>
        protected Pool(PoolSettings<T>? settings, Func<PoolSettings<T>, IPoolStorage<T>> storageInitializer)
        {
            if (storageInitializer == null)
                throw new ArgumentNullException(nameof(storageInitializer));

            PoolSettings<T> settingsValue = settings.HasValue ? settings.Value : default(PoolSettings<T>);
            _initializer = settingsValue.Initializer ?? New<T>.Create;
            _reset = settingsValue.Reset ?? EmptyAction;
            _dispose = (typeof(IDisposable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo())) ? DisposeAction : EmptyAction;

            _storage = storageInitializer(settingsValue);
            _storage.SetCapacity(settingsValue.Capacity);
            for (int i = 0; i < settingsValue.Capacity; i++)
                _storage.Register(_initializer());
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

                case PoolingMode.Flexible:
                    storage = new FlexiblePoolStorage<T>();
                    break;

                default:
                    throw new ArgumentException($"{mode} is not a valid pooling mode");
            }

            return storage;
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Resets the pool
        /// </summary>
        /// <param name="destroyItems">Used to indicates that elements of the pool must be destroyed</param>
        public void Reset(bool destroyItems = false)
        {
            CheckDisposeState();

            _storage.ResetItems();

            Action<T> resetAction = destroyItems ? _dispose : _reset;
            foreach (T item in _storage)
                resetAction(item);

            if (destroyItems)
                _storage.DestroyItems();
        }

        /// <summary>
        /// Disposes the pool
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
                return;

            Dispose(true);
            _isDisposed = true;
        }

        /// <summary>
        /// Disposes the pool
        /// </summary>
        /// <param name="disposing">Indicates that the method is called from the IDisposable.Dispose() method</param>
        protected virtual void Dispose(bool disposing)
        {
            Reset(true);
        }

        private void CheckDisposeState()
        {
            if(_isDisposed)
                throw new ObjectDisposedException(ToString());
        }

        #endregion

        #region Pooling

        /// <summary>
        /// Gets an available element
        /// </summary>
        /// <returns>Return an element from the pool, if no elements are available a new one will be created</returns>
        public PoolItem<T> Get()
        {
            CheckDisposeState();

            T item = _storage.Get();
            if (item != null)
                return new PoolItem<T>(item, this);

            _storage.Register(_initializer());
            item = _storage.Get();

            return new PoolItem<T>(item, this);
        }

        /// <summary>
        /// Returns an element to the pool
        /// </summary>
        /// <param name="item">Represents an element that must be returned</param>
        public void Set(T item)
        {
            CheckDisposeState();

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _reset(item);
            _storage.Set(item);
        }

        /// <summary>
        /// Returns a collection of elements to the pool
        /// </summary>
        /// <param name="items">The collection of elements that must be returned</param>
        public void Set(IEnumerable<T> items)
        {
            CheckDisposeState();

            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach(T item in items)
                Set(item);
        }

        #endregion
    }
}