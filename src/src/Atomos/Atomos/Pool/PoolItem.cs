using System;

namespace Atomos
{
    /// <summary>
    /// Represents a wrapper around a pool item to ease item release
    /// </summary>
    /// <typeparam name="T">Type of pool item</typeparam>
    public struct PoolItem<T> : IDisposable where T : class
    {
        #region Fields

        private readonly Pool<T> _pool;
        private bool _isDisposed;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the item value
        /// </summary>
        public T Item { get; }

        #endregion

        #region Constructors

        internal PoolItem(T item, Pool<T> pool)
        {
            Item = item;
            _pool = pool;
            _isDisposed = false;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Cast the current instance to return the wrapped item
        /// </summary>
        /// <param name="wrapper">Current PoolItem instance</param>
        public static implicit operator T(PoolItem<T> wrapper)
        {
            return wrapper._isDisposed ? null : wrapper.Item;
        }

        #endregion

        #region Release

        /// <summary>
        /// Disposes the wrapper and release the item to the pool
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _pool.Set(Item);
        }

        #endregion
    }
}