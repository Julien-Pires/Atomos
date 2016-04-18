using System;
using System.Collections.Generic;

namespace Atomos
{
    /// <summary>
    /// Represents a pool of items
    /// </summary>
    /// <typeparam name="T">Type of item</typeparam>
    public interface IPool<T> : IDisposable 
        where T : class
    {
        #region Properties

        /// <summary>
        /// Gets the total available items
        /// </summary>
        int Count { get; }

        #endregion

        #region Cleanup

        /// <summary>
        /// Resets the pool
        /// </summary>
        /// <param name="destroyItems">Indicates that items must be destroyed</param>
        void Reset(bool destroyItems = false);

        #endregion

        #region Pooling

        /// <summary>
        /// Gets an available element
        /// </summary>
        /// <returns>Return an element from the pool, if no elements are available a new one will be created</returns>
        PoolItem<T> Get();

        /// <summary>
        /// Returns an element to the pool
        /// </summary>
        /// <param name="item">Represents an element that must be returned</param>
        void Set(T item);

        /// <summary>
        /// Returns a collection of elements to the pool
        /// </summary>
        /// <param name="items">The collection of elements that must be returned</param>
        void Set(IEnumerable<T> items);

        #endregion
    }
}
