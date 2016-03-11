using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test_Flexible<T>
    {
		[Theory]
		[InlineData(10)]
        [InlineData(100)]
        [InlineData(5000)]
        public void Set_UnknownSingleItem(int value)
        {
            _pool.Set(new T { Value = value });

            Assert.Equal(value, _pool.Get().Value);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(5000)]
        public void Set_UnknownMultipleItems(int value)
        {
            _pool.Set(new[] { new T { Value = value } });

            Assert.Equal(value, _pool.Get().Value);
        }
    }
}