﻿namespace Atomos
{
    internal class StrictPoolGuard<T> : IPoolGuard<T> where T : class
    {
        #region Guards

        public bool MustRegister()
        {
            return true;
        }

        public bool CanGet(IPoolStorage<T> storage)
        {
            return true;
        }

        public bool CanSet(T item, IPoolStorage<T> storage)
        {
            return storage.IsRegistered(item) && !storage.IsAvailable(item);
        }

        #endregion
    }
}