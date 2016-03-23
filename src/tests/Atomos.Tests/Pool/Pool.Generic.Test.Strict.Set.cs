using System;

using Xunit;

namespace Atomos.Tests.Pool
{
    public abstract partial class Pool_Generic_Test_Strict<TPool, T>
    {
        [Fact]
        public void Set_UnknownSingleItem_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => Pool.Set(new T()));
        }

        [Fact]
        public void Set_UnknownMultipleItems_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => Pool.Set(new []{ new T() }));
        }
    }
}
