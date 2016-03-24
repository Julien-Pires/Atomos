using System;

namespace Atomos
{
    /// <summary>
    /// Represents a set of pool parameters used to initialize the pool with custom behaviors
    /// </summary>
    /// <typeparam name="T">Type of pool elements</typeparam>
    public struct PoolSettings<T> where T : class
    {
        #region Properties

        /// <summary>
        /// Gets or sets the pooling mode
        /// </summary>
        public PoolingMode Mode { get; set; }

        /// <summary>
        /// Gets or sets the initial capacity
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the function used to create pool elements
        /// </summary>
        public Func<T> Initializer { get; set; }

        /// <summary>
        /// Gets or sets the function used to reset pool elements when they are returned
        /// </summary>
        public Action<T> Reset { get; set; }

        #endregion
    }
}