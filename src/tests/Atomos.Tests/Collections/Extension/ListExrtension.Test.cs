using System;
using System.Linq;
using System.Collections.Generic;
using Atomos.Collections.Extension;
using Xunit;

namespace Atomos.Tests.Collections.Extension
{
    public class ListExtension_Test
    {
        #region Binary Search Tests

        [Fact]
        public void BinarySearch_WhenNullSource_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ListExtension.BinarySearch<int, int>(null, 0, (c, d) => 0));
        }

        [Fact]
        public void BinarySearch_WhenNullComparer_ThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new List<int>().BinarySearch(0, (Func<int, int, int>)null));
        }

        [Theory]
        [InlineData(new[] { 0, 0, 0, 0, 0 }, 0, 2)]
        [InlineData(new[] { 0, 10, 100, 1000 }, 100, 2)]
        [InlineData(new[] { -20, -10, -2, -1 }, -1, 3)]
        [InlineData(new[] { -10, -2, -1, 20 }, -10, 0)]
        public void BinarySearch_WhenContainsPrimitiveElement_FindWithSuccess(IList<int> source, int value, int index)
        {
            int result = source.BinarySearch(value, Comparer<int>.Default.Compare);

            Assert.Equal(index, result);
        }

        [Theory]
        [InlineData(new[] { 0, 0, 0, 0, 0 }, 1, 5)]
        [InlineData(new[] { 0, 10, 100, 1000 }, 7777, 4)]
        [InlineData(new[] { -20, -10, -1, 0 }, -100, 0)]
        public void BinarySearch_WhenDoesntContainsPrimitiveElement_FindBestIndexWithSuccess(IList<int> source, int value, int index)
        {
            int result = source.BinarySearch(value, Comparer<int>.Default.Compare);

            Assert.Equal(index, ~result);
        }

        [Theory]
        [InlineData(new[] {"a", "b", "c", "d", "e"}, "c", 2)]
        [InlineData(new[] {"abc", "def", "mno", "pqr"}, "abc", 0)]
        public void BinarySearch_WhenContainsComplexElement_FindWithSuccess(IList<string> source, string value, int index)
        {
            List<Tuple<string>> list = source.Select(c => new Tuple<string>(c)).ToList();

            int result = list.BinarySearch(value, (c, d) => Comparer<string>.Default.Compare(c.Item1, d));

            Assert.Equal(index, result);
        }

        [Theory]
        [InlineData(new[] { "a", "b", "c", "d", "e" }, "z", 5)]
        [InlineData(new[] { "abc", "def", "mno", "pqr" }, "Foo", 2)]
        public void BinarySearch_WhenDoesntContainsComplexElement_FindWithSuccess(IList<string> source, string value, int index)
        {
            List<Tuple<string>> list = source.Select(c => new Tuple<string>(c)).ToList();

            int result = list.BinarySearch(value, (c, d) => Comparer<string>.Default.Compare(c.Item1, d));

            Assert.Equal(index, ~result);
        }

        #endregion
    }
}