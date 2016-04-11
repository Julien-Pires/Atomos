using System;

namespace Atomos
{
    public class PoolException : Exception
    {
        #region Constructors

        public PoolException()
        {
        }

        public PoolException(string message) : base(message)
        {
        }

        #endregion
    }
}