namespace Atomos
{
    /// <summary>
    /// Represents the pool return policy
    /// </summary>
    public enum PoolingMode
    {
        /// <summary>
        /// Value used to indicates that only element created by the pool can be returned
        /// </summary>
        Strict = 0,
        /// <summary>
        /// Value used to indicates that any object can be returned to the pool
        /// </summary>
        Flexible = 1
    }
}
