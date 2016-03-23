using System;
using System.Collections.Generic;

using Atomos.Atomos;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<TPool, T>
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 1)]
        [InlineData(100, 1)]
        [InlineData(50000, 1)]
        public void Set_SingleItems(int itemsCount, int count)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                PoolItem<T> item = Pool.Get();
                Pool.Set(item);
            }

            Assert.Equal(count, Pool.Count);
        }

        [Fact]
        public void Set_SingleItemTwice_ThrowException()
        {
            PoolItem<T> item = Pool.Get();
            Pool.Set(item);

            Assert.Throws<ArgumentException>(() => Pool.Set(item));
        }

        [Fact]
        public void Set_NullSingleItems_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => Pool.Set((T)null));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 1)]
        [InlineData(100, 1)]
        [InlineData(50000, 1)]
        public void Set_MultipleItems(int itemsCount, int count)
        {
            for(int i = 0; i < itemsCount; i++)
            {
                T[] items = { Pool.Get() };
                Pool.Set(items);
            }

            Assert.Equal(count, Pool.Count);
        }

        [Fact]
        public void Set_MultipleItemTwice_ThrowException()
        {
            T item = Pool.Get();
            
            Assert.Throws<ArgumentException>(() => Pool.Set(new []{ item, item }));
        }

        [Fact]
        public void Set_NullMultipleItems_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => Pool.Set((IEnumerable<T>)null));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        [InlineData(50000, 50000)]
        public void Set_SingleItems_TotalAvailable(int itemsCount, int count)
        {
            List<PoolItem<T>> itemsList = new List<PoolItem<T>>(itemsCount);
            for (int i = 0; i < itemsCount; i++)
                itemsList.Add(Pool.Get());

            for(int i = 0; i < itemsCount; i++)
                Pool.Set(itemsList[i]);

            Assert.Equal(count, Pool.Count);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        [InlineData(50000, 50000)]
        public void Set_MultipleItems_TotalAvailable(int itemsCount, int count)
        {
            List<T> itemsList = new List<T>(itemsCount);
            for (int i = 0; i < itemsCount; i++)
                itemsList.Add(Pool.Get());

            Pool.Set(itemsList);

            Assert.Equal(count, Pool.Count);
        }
    }
}