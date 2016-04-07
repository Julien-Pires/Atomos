namespace Atomos
{
    public interface IPoolItemInitializer<out TItem, in TParameter> where TItem : class
    {
        #region Methods

        TItem Create(TParameter parameter);

        #endregion
    }
}