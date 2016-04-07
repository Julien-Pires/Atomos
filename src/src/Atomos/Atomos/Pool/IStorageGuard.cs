namespace Atomos
{
    internal interface IStorageGuard<TItem> : IPoolGuard<TItem> where TItem : class
    {
        /// <summary>
        /// Indicates that item registration is required on the pool storage
        /// </summary>
        /// <returns>Returns true if the item must be registered otherwise false</returns>
        bool MustRegister();
    }
}