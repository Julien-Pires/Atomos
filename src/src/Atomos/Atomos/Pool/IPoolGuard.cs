namespace Atomos
{
    /// <summary>
    /// Represents a guard proxy between a pool storage and a pool
    /// </summary>
    public interface IPoolGuard
    {
        #region Methods

        /// <summary>
        /// Indicates that an item can be get from the pool storage
        /// </summary>
        /// <param name="storage">Storage used to perform the get operation</param>
        /// <returns>Returns true if the get operation can be performed otherwise false</returns>
        bool CanGet<T>(IPoolStorage<T> storage) where T : class;

        /// <summary>
        /// Indicates that an item can be set on the pool storage
        /// </summary>
        /// <param name="item">Item to set</param>
        /// <param name="storage">Storage used to perform the set operation</param>
        /// <returns>Returns true if the set operation can be performed otherwise false</returns>
        bool CanSet<T>(T item, IPoolStorage<T> storage) where T : class;

        #endregion
    }
}