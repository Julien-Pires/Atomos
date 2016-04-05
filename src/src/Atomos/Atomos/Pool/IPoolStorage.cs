using System.Collections.Generic;

namespace Atomos
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

        T this[int index] { get; }

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
        /// <param name="item">Represents the item to register</param>
        void Register(T item);

        /// <summary>
        /// Indicates that the item is registered to this storage
        /// </summary>
        /// <param name="item">Represents the item to test registration</param>
        /// <returns>Returns true if the item is registered to this storage otherwise false</returns>
        bool IsRegistered(T item);

        /// <summary>
        /// Indicates that an item is available or not
        /// </summary>
        /// <param name="item">Represents the item to test availability</param>
        /// <returns>Returns true if the item is available otherwise false</returns>
        bool IsAvailable(T item);

        /// <summary>
        /// Resets the storage items state
        /// </summary>
        /// <remarks>
        /// This method must only be used to reset the internal items state (eg: mark items as available)
        /// </remarks>
        void ResetItems();

        /// <summary>
        /// Destroys items in the storage
        /// </summary>
        /// <remarks>
        /// This method must only be used to destroy items in the storage to make it as if it was a freshly 
        /// created instance
        /// </remarks>
        void DestroyItems();

        /// <summary>
        /// Gets an available element
        /// </summary>
        /// <typeparam name="TParam">Type of parameter</typeparam>
        /// <param name="parameter">Additional parameter</param>
        /// <returns>Return an available element if at least one exists otherwise null</returns>
        T Get<TParam>(TParam parameter);

        /// <summary>
        /// Returns an element to the storage
        /// </summary>
        /// <typeparam name="TParam">Type of parameter</typeparam>
        /// <param name="item">Represents the element that must be returned</param>
        /// <param name="parameter">Additional parameter</param>
        void Set<TParam>(T item, TParam parameter);

        #endregion
    }
}
