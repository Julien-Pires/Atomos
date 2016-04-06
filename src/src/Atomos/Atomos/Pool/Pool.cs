﻿using System;
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
    /// <typeparam name="T">Type of pool elements</typeparam>
    public class Pool<T> : IPool<T> where T : class
    {
        #region Fields

        private static readonly IPoolGuard[] EmptyGuards = new IPoolGuard[0];
        private static readonly Action<T> EmptyAction = c => { };
        private static readonly Action<T> DisposeAction = c => (c as IDisposable).Dispose();
        private static readonly Action<T> ResetAction = c => (c as IPoolItem).Reset();
        private static readonly NullPoolStorageParameter NullParameter = new NullPoolStorageParameter();

        private bool _isDisposed;
        private readonly IPoolStorage<T> _storage;
        private readonly IStorageGuard _storageGuard;
        private readonly IPoolGuard[] _poolGuards;
        private readonly IPoolStorageQuery _query;
        private readonly Func<T> _initializer;
        private readonly Action<T> _reset;

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
        static Pool()
        {
            ResetAction = typeof(IPoolItem).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()) ? ResetAction : EmptyAction;
            DisposeAction = typeof(IDisposable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()) ? DisposeAction : EmptyAction;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="Pool{T}"/> with the specified parameters
        /// </summary>
        /// <param name="settings">Pool parameters</param>
        public Pool(PoolSettings<T> settings = null) : this(settings, null, null, null)
        {
        }

        protected Pool(PoolSettings<T> settings, IPoolStorage<T> storage, IEnumerable<IPoolGuard> guards, 
            IPoolStorageQuery query)
        {
            _initializer = settings.Initializer ?? New<T>.Create;
            _reset = settings.Reset ?? ResetAction;

            _storageGuard = CreateStorageGuard(settings);
            _poolGuards = guards != null ? guards.ToArray() : EmptyGuards;
            _storage = storage ?? new PoolStorage<T>();
            _query = query ?? DefaultPoolStorageQuery.Default;

            _storage.SetCapacity(settings.Capacity);
            for (int i = 0; i < settings.Capacity; i++)
            {
                T item = _initializer();
                if (_storageGuard.MustRegister())
                    _storage.Register(item);

                _storage.Set(item, NullParameter);
            }
        }

        #endregion

        #region Initialize

        private static IStorageGuard CreateStorageGuard(PoolSettings<T> settings)
        {
            PoolingMode mode = settings.Mode;
            IStorageGuard storageGuard;
            switch(mode)
            {
                case PoolingMode.Strict:
                    storageGuard = new StrictPoolGuard();
                    break;

                case PoolingMode.Flexible:
                    storageGuard = new FlexiblePoolGuard();
                    break;

                default:
                    throw new ArgumentException($"{mode} is not a valid pooling mode");
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

            Action<T> resetAction = destroyItems ? DisposeAction : _reset;
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

            return Get(NullParameter);
        }

        /// <summary>
        /// Returns an element to the pool
        /// </summary>
        /// <param name="item">Represents an element that must be returned</param>
        public void Set(T item)
        {
            CheckDisposeState();

            Set(item, NullParameter);
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

        /// <summary>
        /// Returns an element to the pool 
        /// </summary>
        /// <typeparam name="TParam">Type of parameter</typeparam>
        /// <param name="item">Represents an element that must be returned</param>
        /// <param name="parameter">Additional parameter</param>
        protected void Set<TParam>(T item, TParam parameter)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!_storageGuard.CanSet(item, _storage))
                throw new PoolException($"Failed to set {item}, guard rules not fullfilled");

            for (int i = 0; i < _poolGuards.Length; i++)
            {
                if(!_poolGuards[i].CanSet(item, _storage))
                    throw new PoolException($"Failed to set {item}, guard rules not fullfilled");
            }

            _reset(item);
            _storage.Set(item, parameter);
        }

        /// <summary>
        /// Gets an available element
        /// </summary>
        /// <typeparam name="TParam">Type of paramater</typeparam>
        /// <param name="parameter">Additional parameter</param>
        /// <returns>Return an element from the pool, if no elements are available a new one will be created</returns>
        protected PoolItem<T> Get<TParam>(TParam parameter)
        {
            if (!_storageGuard.CanGet(_storage))
                throw new PoolException("Failed to get an item, guard rules not fullfilled");

            for (int i = 0; i < _poolGuards.Length; i++)
            {
                if(!_poolGuards[i].CanGet(_storage))
                    throw new PoolException("Failed to get an item, guard rules not fullfilled");
            }

            T item = _query.Search(_storage, parameter);
            if (item != null)
                return new PoolItem<T>(item, this);

            item = _initializer();
            if (_storageGuard.MustRegister())
                _storage.Register(item);

            return new PoolItem<T>(item, this);
        }

        #endregion
    }
}