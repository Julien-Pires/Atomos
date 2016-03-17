using System;
using System.Collections.Generic;

namespace Atomos.Atomos
{
    public static class SharedPool<T> where T : class
    {
        #region Fields

        private static readonly Dictionary<string, Pool<T>> _pools;

        public static readonly Pool<T> Default = new Pool<T>();

        #endregion

        #region Constructors

        static SharedPool()
        {
            _pools = new Dictionary<string, Pool<T>>();
        }

        #endregion

        #region Pool Management

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

        public static void DestroyAll()
        {
            foreach (Pool<T> pool in _pools.Values)
                pool.Dispose();

            _pools.Clear();
        }

        #endregion
    }
}