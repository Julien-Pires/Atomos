using System;

namespace Atomos.Tests.Pool
{
    public abstract class ArrayPool_Generic_Test_Any<T> : CollectionPool_Generic_Test_Any<ArrayPool<T>, T[]>
    {
        protected ArrayPool_Generic_Test_Any()
            : base(c => new ArrayPool<T>(c))
        {
        }

        protected override T[] CreateItem(int capacity) => new T[capacity];

        protected override int GetCapacity(T[] pool) => pool.Length;
    }

    public abstract class ArrayPool_Generic_Test_Definite<T> : CollectionPool_Generic_Test_Definite<ArrayPool<T>, T[]>
    {
        protected ArrayPool_Generic_Test_Definite()
            : base(c => new ArrayPool<T>(c))
        {
        }

        protected override T[] CreateItem(int capacity) => new T[capacity];

        protected override int GetCapacity(T[] pool) => pool.Length;
    }

    public abstract class ArrayPool_Generic_Test_Fixed<T> : CollectionPool_Generic_Test_Any<ArrayPool<T>, T[]>
    {
        protected ArrayPool_Generic_Test_Fixed()
            : base(c => new ArrayPool<T>(c))
        {
        }

        protected override T[] CreateItem(int capacity) => new T[capacity];

        protected override int GetCapacity(T[] pool) => pool.Length;
    }
}