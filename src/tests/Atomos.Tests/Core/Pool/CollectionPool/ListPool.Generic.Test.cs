using System.Collections.Generic;

namespace Atomos.Tests.Pool
{
    public abstract class ListPool_Generic_Test<T> : CollectionPool_Generic_Test<ListPool<T>, List<T>>
    {
        #region Constructors

        protected ListPool_Generic_Test()
            : base(c => new ListPool<T>((CollectionPoolSettings<List<T>>)c))
        {
        }

        #endregion

        #region Methods

        protected override int GetValue(List<T> item) => item.Count;

        protected override void ResetItem(List<T> item)
        {
        }

        protected override List<T> CreateItem(int capacity) => new List<T>(capacity);

        protected override int GetCapacity(List<T> item) => item.Capacity;

        #endregion
    }
}