namespace Atomos
{
    /// <summary>
    /// Represents a guard proxy between a pool storage and a pool
    /// </summary>
    /// <typeparam name="T">Type of pool item</typeparam>
    public interface IPoolGuard<T> where T : class
    {
        #region Methods

        /// <summary>
        /// Indicates that item registration is required on the pool storage
        /// </summary>
        /// <returns>Returns true if the item must be registered otherwise false</returns>
        bool MustRegister();

        /// <summary>
        /// Indicates that an item can be get from the pool storage
        /// </summary>
        /// <param name="storage">Storage used to perform the get operation</param>
        /// <returns>Returns true if the get operation can be performed otherwise false</returns>
        bool CanGet(IPoolStorage<T> storage);

        /// <summary>
        /// Indicates that an item can be set on the pool storage
        /// </summary>
        /// <param name="item">Item to set</param>
        /// <param name="storage">Storage used to perform the set operation</param>
        /// <returns>Returns true if the set operation can be performed otherwise false</returns>
        bool CanSet(T item, IPoolStorage<T> storage);

        #endregion
    }
}