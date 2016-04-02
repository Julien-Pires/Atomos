namespace Atomos.Tests.Pool
{
    public abstract class ArrayPool_Generic_Test<T> : CollectionPool_Generic_Test<ArrayPool<T>, T[]>
    {
        protected ArrayPool_Generic_Test()
            : base(c => new ArrayPool<T>(c))
        {
        }

        protected override T[] CreateItem(int capacity) => new T[capacity];

        protected override int GetCapacity(T[] pool) => pool.Length;
    }
}