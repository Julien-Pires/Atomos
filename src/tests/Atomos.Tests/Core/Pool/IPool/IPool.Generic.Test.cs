namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
        where TPool : IPool<T>
        where T : class
    {
        #region Fields

        protected readonly IPool_Builder<TPool, T> Builder;

        #endregion

        #region Constructors

        protected IPool_Generic_Test(IPool_Builder<TPool, T> builder)
        {
            Builder = builder;
        }

        #endregion

        #region Builder

        protected TPool Build()
        {
            return Builder.Build();
        }

        #endregion

        #region Items Validation

        protected abstract bool IsDisposed(T item);

        protected abstract bool IsReset(T item);

        #endregion
    }
}