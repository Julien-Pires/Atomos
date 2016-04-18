using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Atomos
{
    /// <summary>
    /// Implements a generic object pool. The pool will automatically create new elements
    /// when required. By default the pool return policy is "Strict", it means that a
    /// pool can only accept element that has been created by the pool. "Flexible" mode allow
    /// to return any object to the pool.
    /// </summary>
    /// <typeparam name="TItem">Type of pool elements</typeparam>
    /// <typeparam name="TParam">Type of pool parameters</typeparam>
    public abstract class BasePool<TItem, TParam> : IPool<TItem> where TItem : class
    {
        #region Fields

        private static readonly Action<TItem> EmptyAction = c => { };
        private static readonly Action<TItem> DisposeAction = c => (c as IDisposable)?.Dispose();
        private static readonly Action<TItem> ResetAction = c => (c as IPoolItem)?.Reset();

        private bool _isDisposed;
        private readonly IPoolStorage<TItem> _storage;
        private readonly IStorageGuard<TItem> _storageGuard;
        private readonly IPoolGuard<TItem>[] _poolGuards;
        private readonly IPoolStorageQuery<TItem, TParam> _query;
        private readonly IPoolItemFactory<TItem, TParam> _itemFactory;
        private readonly Action<TItem> _reset;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of available elements in the pool
        /// </summary>
        public int Count => _storage.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize the Pool type
        /// </summary>
        static BasePool()
        {
            ResetAction = typeof(IPoolItem).GetTypeInfo().IsAssignableFrom(typeof(TItem).GetTypeInfo()) ? ResetAction : EmptyAction;
            DisposeAction = typeof(IDisposable).GetTypeInfo().IsAssignableFrom(typeof(TItem).GetTypeInfo()) ? DisposeAction : EmptyAction;
        }

        protected BasePool(PoolSettings<TItem> settings, IPoolStorage<TItem> storage, IPoolStorageQuery<TItem, TParam> query,
            IEnumerable<IPoolGuard<TItem>> guards = null, IPoolItemFactory<TItem, TParam> itemFactory = null)
        {
            settings = ValidateSettings(settings) ?? new PoolSettings<TItem>();

            _itemFactory = itemFactory ?? new DefaultPoolItemFactory<TItem, TParam>(settings.Initializer ?? New<TItem>.Create);
            _reset = settings.Reset ?? ResetAction;

            _storageGuard = CreateStorageGuard(settings);
            _poolGuards = guards != null ? guards.Concat(new [] { _storageGuard }).ToArray() : new IPoolGuard<TItem>[] { _storageGuard };
            _storage = storage;
            _query = query;

            _storage.SetCapacity(settings.Capacity);
            for (int i = 0; i < settings.Capacity; i++)
            {
                TItem item = _itemFactory.Create(default(TParam));
                if (_storageGuard.MustRegister())
                    _storage.Register(item);

                _storage.Set(item);
            }
        }

        #endregion

        #region Validation

        private static PoolSettings<TItem> ValidateSettings(PoolSettings<TItem> settings)
        {
            if (settings == null)
                return null;

            if (settings.Capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(settings.Capacity), "Initial pool capacity must be positive");

            if (Enum.GetValues(typeof(PoolingMode)).Cast<PoolingMode>().All(c => c != settings.Mode))
                throw new PoolException($"{settings.Mode} is not a valid pooling mode");

            return settings;
        } 

        #endregion

        #region Initialize

        private static IStorageGuard<TItem> CreateStorageGuard(PoolSettings<TItem> settings)
        {
            PoolingMode mode = settings.Mode;
            IStorageGuard<TItem> storageGuard;
            switch(mode)
            {
                case PoolingMode.Strict:
                    storageGuard = new StrictPoolGuard<TItem>();
                    break;

                case PoolingMode.Flexible:
                    storageGuard = new FlexiblePoolGuard<TItem>();
                    break;

                default:
                    throw new PoolException($"{mode} is not a valid pooling mode");
            }

            return storageGuard;
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

            Action<TItem> resetAction = destroyItems ? DisposeAction : _reset;
            foreach (TItem item in _storage)
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
        public PoolItem<TItem> Get()
        {
            return Get(default(TParam));
        }

        /// <summary>
        /// Returns an element to the pool
        /// </summary>
        /// <param name="item">Represents an element that must be returned</param>
        public void Set(TItem item)
        {
            Set(item, default(TParam));
        }

        /// <summary>
        /// Returns a collection of elements to the pool
        /// </summary>
        /// <param name="items">The collection of elements that must be returned</param>
        public void Set(IEnumerable<TItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach(TItem item in items)
                Set(item);
        }

        /// <summary>
        /// Returns an element to the pool 
        /// </summary>
        /// <typeparam name="TParam">Type of parameter</typeparam>
        /// <param name="item">Represents an element that must be returned</param>
        /// <param name="parameter">Additional parameter</param>
        protected void Set(TItem item, TParam parameter)
        {
            CheckDisposeState();

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (_poolGuards.Any(t => !t.CanSet(item, _storage)))
                throw new PoolException($"Failed to set {item}, guard rules not fullfilled");

            _reset(item);
            _query.Insert(_storage, item, parameter);
        }

        /// <summary>
        /// Gets an available element
        /// </summary>
        /// <typeparam name="TParam">Type of paramater</typeparam>
        /// <param name="parameter">Additional parameter</param>
        /// <returns>Return an element from the pool, if no elements are available a new one will be created</returns>
        protected PoolItem<TItem> Get(TParam parameter)
        {
            CheckDisposeState();

            if (_poolGuards.Any(t => !t.CanGet(_storage)))
                throw new PoolException("Failed to get an item, guard rules not fullfilled");

            TItem item = _query.Search(_storage, parameter);
            if (item != null)
                return new PoolItem<TItem>(item, this);

            item = _itemFactory.Create(parameter);
            if (_storageGuard.MustRegister())
                _storage.Register(item);

            return new PoolItem<TItem>(item, this);
        }

        #endregion
    }
}