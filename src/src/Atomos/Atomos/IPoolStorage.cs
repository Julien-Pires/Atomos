using System.Collections.Generic;

namespace Atomos.Atomos
{
    /// <summary>
    /// Represents a pool storage used to store elements
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPoolStorage<T> : IEnumerable<T> where T : class
    {
        #region Properties

        /// <summary>
        /// Gets the number of available elements
        /// </summary>
        int Count { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the storage capacity
        /// </summary>
        /// <param name="capacity">Represents the storage capacity</param>
        void SetCapacity(int capacity);

        /// <summary>
        /// Registers a new element to the storage
        /// </summary>
        /// <param name="item"></param>
        void Register(T item);

        /// <summary>
        /// Resets the storage
        /// </summary>
        /// <param name="destroyItems">Indicates that elements must be destroyed</param>
        void Reset(bool destroyItems);

        /// <summary>
        /// Gets an available element
        /// </summary>
        /// <returns>Return an available element if at least one exists otherwise null</returns>
        T Get();

        /// <summary>
        /// Returns an element to the storage
        /// </summary>
        /// <param name="item">Represents the element that must be returned</param>
        void Set(T item);

        #endregion
    }
}
