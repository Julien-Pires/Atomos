using System;

namespace Atomos.Atomos
{
    public struct PoolItem<T> : IDisposable where T : class
    {
        #region Fields

        private readonly Pool<T> _pool;
        private bool _isDisposed;

        #endregion

        #region Properties

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

        public static implicit operator T(PoolItem<T> wrapper)
        {
            if (wrapper._isDisposed)
                throw new InvalidOperationException($"Failed to cast to {typeof(T)} because instance has been disposed");

            return  wrapper.Item;
        }

        #endregion

        #region Release

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