namespace Atomos
{
    internal class FlexiblePoolGuard : IStorageGuard
    {
        #region Guards

        public bool MustRegister()
        {
            return false;
        }

        public bool CanGet<T>(IPoolStorage<T> storage) where T : class
        {
            return true;
        }

        public bool CanSet<T>(T item, IPoolStorage<T> storage) where T : class
        {
            return !storage.IsAvailable(item);
        }

        #endregion
    }
}