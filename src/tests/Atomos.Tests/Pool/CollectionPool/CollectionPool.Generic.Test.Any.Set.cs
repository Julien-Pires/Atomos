using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test_Any<TPool, TItem>
    {
        [Theory]
        [InlineData(new[] { 10 })]
        [InlineData(new[] { 10, 10, 100 })]
        [InlineData(new [] { 10, 100, 200, 500 })]
        public void Set_AnyItem_WithSuccess(int[] sizes)
        {
            List<TItem> items = sizes.Select(c => Pool.Get(c)).ToList();

            Pool.Set(items);
        }
    }
}