using System;

namespace Atomos.Atomos
{
    public static class SharedPool<T> where T : class
    {
        #region Fields

        public static readonly Pool<T> Default;

        #endregion

        #region Pool Management

        public static Pool<T> Get(string name, PoolSettings<T>? settings = null)
        {
            throw new NotImplementedException();
        }

        public static bool Destroy(string name)
        {
            throw new NotImplementedException();
        }

        public static void DestroyAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}