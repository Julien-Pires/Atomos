using System;

namespace Atomos
{
    internal class StrictPoolGuard<T> : IPoolGuard<T> where T : class
    {
        #region Guards

        public bool CanGet(IPoolStorage<T> storage)
        {
            return false;
        }

        public bool CanSet(T item, IPoolStorage<T> storage)
        {
            return false;
        }

        #endregion
    }
}