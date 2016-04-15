using System.Collections.Generic;

namespace Atomos
{
    public sealed partial class ListPool<TItem>
    {
        public class ListPoolHelper : ICollectionPoolHelper<List<TItem>>
        {
            #region Helpers

            public int GetCapacity(List<TItem> item)
            {
                return item.Capacity;
            }

            #endregion
        }
    }
}