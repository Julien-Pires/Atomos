namespace Atomos
{
    internal class StrictPoolGuard : IStorageGuard
    {
        #region Guards

        public bool MustRegister()
        {
            return true;
        }

        public bool CanGet<T>(IPoolStorage<T> storage) where T : class
        {
            return true;
        }

        public bool CanSet<T>(T item, IPoolStorage<T> storage) where T : class
        {
            return storage.IsRegistered(item) && !storage.IsAvailable(item);
        }

        #endregion
    }
}