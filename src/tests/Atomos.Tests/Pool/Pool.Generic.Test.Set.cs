using System;
using System.Collections.Generic;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test<T>
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
                T item = _pool.Get();
                _pool.Set(item);
            }

            Assert.Equal(count, _pool.Count);
        }

        [Fact]
        public void Set_NullSingleItems_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _pool.Set((T)null));
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
                T[] items = { _pool.Get() };
                _pool.Set(items);
            }

            Assert.Equal(count, _pool.Count);
        }

        [Fact]
        public void Set_NullMultipleItems_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => _pool.Set((IEnumerable<T>)null));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 10)]
        [InlineData(100, 100)]
        [InlineData(50000, 50000)]
        public void Set_SingleItems_TotalAvailable(int itemsCount, int count)
        {
            List<T> itemsList = new List<T>(itemsCount);
            for (int i = 0; i < itemsCount; i++)
                itemsList.Add(_pool.Get());

            for(int i = 0; i < itemsCount; i++)
                _pool.Set(itemsList[i]);

            Assert.Equal(count, _pool.Count);
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
                itemsList.Add(_pool.Get());

            _pool.Set(itemsList);

            Assert.Equal(count, _pool.Count);
        }
    }
}