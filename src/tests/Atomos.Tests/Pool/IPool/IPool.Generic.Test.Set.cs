using System;
using System.Collections.Generic;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class IPool_Generic_Test<TPool, T>
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 1)]
        [InlineData(100, 1)]
        [InlineData(50000, 1)]
        public void Set_SingleItems(int itemsCount, int count)
        {
            TPool pool = Build();

            for (int i = 0; i < itemsCount; i++)
            {
                PoolItem<T> item = pool.Get();
                pool.Set(item);
            }

            Assert.Equal(count, pool.Count);
        }

        [Fact]
        public void Set_SingleItemTwice_ThrowException()
        {
            TPool pool = Build();

            PoolItem<T> item = pool.Get();
            pool.Set(item);

            Assert.Throws<PoolException>(() => pool.Set(item));
        }

        [Fact]
        public void Set_NullSingleItems_ThrowException()
        {
            TPool pool = Build();

            Assert.Throws<ArgumentNullException>(() => pool.Set((T)null));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 1)]
        [InlineData(100, 1)]
        [InlineData(50000, 1)]
        public void Set_MultipleItems(int itemsCount, int count)
        {
            TPool pool = Build();

            for (int i = 0; i < itemsCount; i++)
            {
                T[] items = { pool.Get() };
                pool.Set(items);
            }

            Assert.Equal(count, pool.Count);
        }

        [Fact]
        public void Set_MultipleItemTwice_ThrowException()
        {
            TPool pool = Build();

            T item = pool.Get();
            
            Assert.Throws<PoolException>(() => pool.Set(new []{ item, item }));
        }

        [Fact]
        public void Set_NullMultipleItems_ThrowException()
        {
            TPool pool = Build();

            Assert.Throws<ArgumentNullException>(() => pool.Set((IEnumerable<T>)null));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        [InlineData(50000, 50000)]
        public void Set_SingleItems_TotalAvailable(int itemsCount, int count)
        {
            TPool pool = Build();

            List<PoolItem<T>> itemsList = new List<PoolItem<T>>(itemsCount);
            for (int i = 0; i < itemsCount; i++)
                itemsList.Add(pool.Get());

            for(int i = 0; i < itemsCount; i++)
                pool.Set(itemsList[i]);

            Assert.Equal(count, pool.Count);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        [InlineData(50000, 50000)]
        public void Set_MultipleItems_TotalAvailable(int itemsCount, int count)
        {
            TPool pool = Build();

            List<T> itemsList = new List<T>(itemsCount);
            for (int i = 0; i < itemsCount; i++)
                itemsList.Add(pool.Get());

            pool.Set(itemsList);

            Assert.Equal(count, pool.Count);
        }
    }
}