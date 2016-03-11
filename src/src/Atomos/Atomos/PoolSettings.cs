using System;

namespace Atomos.Atomos
{
    public struct PoolSettings<T> where T : class
    {
        #region Properties

        public PoolingMode Mode { get; set; }

        public int Capacity { get; set; }

        public Func<T> Initializer { get; set; }

        public Action<T> Reset { get; set; }

        #endregion
    }
}