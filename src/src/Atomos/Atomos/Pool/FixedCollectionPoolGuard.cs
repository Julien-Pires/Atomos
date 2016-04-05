using System;

namespace Atomos
{
    internal class FixedCollectionPoolGuard : IPoolGuard
    {
        #region Fields

        private readonly int _capacity;
        private readonly Func<object, int> _getCapacity;

        #endregion

        #region Constructors

        public FixedCollectionPoolGuard(int capacity, Func<object, int> getCapacity)
        {
            _capacity = capacity;
            _getCapacity = getCapacity;
        }

        #endregion

        #region Guard

        bool IPoolGuard.CanGet<T>(IPoolStorage<T> storage)
        {
            return true;
        }

        bool IPoolGuard.CanSet<T>(T item, IPoolStorage<T> storage)
        {
            return _getCapacity(item) == _capacity;
        }

        bool IPoolGuard.MustRegister()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}