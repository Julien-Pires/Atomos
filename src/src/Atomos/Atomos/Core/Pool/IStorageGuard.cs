namespace Atomos
{
    internal interface IStorageGuard
    {
        /// <summary>
        /// Indicates that item registration is required on the pool storage
        /// </summary>
        /// <returns>Returns true if the item must be registered otherwise false</returns>
        bool MustRegister();
    }
}