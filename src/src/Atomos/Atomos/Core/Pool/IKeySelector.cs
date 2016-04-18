namespace Atomos
{
    public interface IKeySelector<in TItem, out TKey> 
        where TItem : class
    {
        #region Methods

        TKey GetKey(TItem item);

        #endregion
    }
}