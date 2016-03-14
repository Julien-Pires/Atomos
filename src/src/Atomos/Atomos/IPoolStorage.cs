using System.Collections.Generic;

namespace Atomos.Atomos
{
    public interface IPoolStorage<T> : IEnumerable<T> where T : class
    {
        #region Properties

        int Count { get; }

        #endregion

        #region Methods

        void SetCapacity(int capacity);

        void Register(T item);

        void Reset(bool destroyItems);

        T Get();

        void Set(T item);

        #endregion
    }
}
