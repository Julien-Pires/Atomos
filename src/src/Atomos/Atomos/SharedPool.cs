using System;
using System.Collections.Generic;

namespace Atomos.Atomos
{
    /// <summary>
    /// Represents a collection of pools that his globally shared
    /// </summary>
    /// <typeparam name="T">Type of pool item</typeparam>
    public static class SharedPool<T> where T : class
    {
        #region Fields

        private static readonly Dictionary<string, Pool<T>> _pools;

        /// <summary>
        /// Default shared pool instance
        /// </summary>
        public static readonly Pool<T> Default = new Pool<T>();

        #endregion

        #region Constructors

        static SharedPool()
        {
            _pools = new Dictionary<string, Pool<T>>();
        }

        #endregion

        #region Pool Management

        /// <summary>
        /// Gets a pool with the specified name, if none exists a new one will be created with the specified parameters
        /// </summary>
        /// <param name="name">Name of the pool</param>
        /// <param name="settings">Pool parameters used when a new pool is created</param>
        /// <returns>Returns a shared pool instance</returns>
        public static Pool<T> Get(string name, PoolSettings<T>? settings = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            Pool<T> pool;
            if (_pools.TryGetValue(name, out pool))
                return pool;

            pool = new Pool<T>(settings);
            _pools[name] = pool;

            return pool;
        }

        /// <summary>
        /// Destroys the pool with the specified name
        /// </summary>
        /// <param name="name">Name of the pool</param>
        /// <returns>Returns true if the pool has been destroyed otherwise false</returns>
        public static bool Destroy(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            Pool<T> pool;
            if (!_pools.TryGetValue(name, out pool))
                return false;

            _pools.Remove(name);
            pool.Dispose();

            return true;
        }

        /// <summary>
        /// Destroys all shared pool except for the Default instance
        /// </summary>
        public static void DestroyAll()
        {
            foreach (Pool<T> pool in _pools.Values)
                pool.Dispose();

            _pools.Clear();
        }

        #endregion
    }
}