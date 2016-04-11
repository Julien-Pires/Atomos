using System;
using System.Linq;
using System.Collections.Generic;
using Atomos.Collections.Extension;
using Ploeh.AutoFixture;
using Xunit;

namespace Atomos.Tests.Collections.Extension
{
    public class DictionaryExtension_Test
    {
        #region Merge tests

        [Fact]
        public void Merge_WhenNullTarget_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => DictionaryExtension.Merge(null, new Dictionary<int, int>()));
        }

        [Fact]
        public void Merge_WhenNullSource_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Dictionary<int, int>().Merge(null));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 100)]
        [InlineData(1000, 1000)]
        public void Merge_WhenDictionaryAreIdentical_WithSucess(int count, int total)
        {
            Fixture fixture = new Fixture { RepeatCount = 0 };
            Guid[] guids = fixture.CreateMany<Guid>(count).ToArray();
            Dictionary<Guid, object> target = guids.ToDictionary(c => c, c => (object)null);
            Dictionary<Guid, object> source = guids.ToDictionary(c => c, c => (object)null);

            target.Merge(source);

            Assert.Equal(total, target.Count);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(100, 200)]
        [InlineData(1000, 2000)]
        public void Merge_WhenDictionaryAreDifferent_WithSucess(int count, int total)
        {
            Fixture fixture = new Fixture { RepeatCount = 0 };
            Dictionary<Guid, object> target = fixture.Create<Dictionary<Guid, object>>();
            Dictionary<Guid, object> source = fixture.Create<Dictionary<Guid, object>>();
            target.AddMany(() => new KeyValuePair<Guid, object>(Guid.NewGuid(), null), count);
            source.AddMany(() => new KeyValuePair<Guid, object>(Guid.NewGuid(), null), count);

            target.Merge(source);

            Assert.Equal(total, target.Count);
        }

        #endregion
    }
}