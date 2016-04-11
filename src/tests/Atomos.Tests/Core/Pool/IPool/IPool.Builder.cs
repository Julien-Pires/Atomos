namespace Atomos.Tests.Pool
{
    public interface IPool_Builder<out TPool, TItem>
        where TPool : IPool<TItem>
        where TItem : class
    {
        TPool Build();
    }
}