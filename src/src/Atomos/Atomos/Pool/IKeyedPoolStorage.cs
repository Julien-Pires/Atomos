namespace Atomos
{
    internal interface IKeyedPoolStorage<TItem, in TKey> : IPoolStorage<TItem>
        where TItem : class
    {
        #region Methods

        TItem Get(TKey key);

        #endregion
    }
}