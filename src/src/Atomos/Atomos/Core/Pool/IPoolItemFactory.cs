namespace Atomos
{
    public interface IPoolItemFactory<out TItem, in TParameter> where TItem : class
    {
        #region Methods

        TItem Create(TParameter parameter);

        #endregion
    }
}