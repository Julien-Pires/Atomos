using System;
using System.Collections.Generic;

namespace Atomos.Atomos
{
    public interface IPool<T> : IDisposable where T : class
    {
        #region Properties

        int Count { get; }

        #endregion

        #region Cleanup

        void Reset(bool destroyItems = false);

        #endregion

        #region Pooling

        PoolItem<T> Get();

        void Set(T item);

        void Set(IEnumerable<T> items);

        #endregion
    }
}
